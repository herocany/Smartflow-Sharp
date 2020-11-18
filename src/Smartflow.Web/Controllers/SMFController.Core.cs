using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;
using Smartflow.Web.Code;
using Smartflow.Web.Models;

namespace Smartflow.Web.Controllers
{
    public class SMFController : ApiController
    {
        private readonly AbstractBridgeService _abstractService;
        private readonly IPendingService _pendingService;
        private readonly IBridgeService _bridgeService;

        protected IWorkflowNodeService NodeService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>().NodeService;
            }
        }

        public SMFController(AbstractBridgeService abstractService, IPendingService pendingService, IBridgeService bridgeService)
        {
            _abstractService = abstractService;
            _pendingService = pendingService;
            _bridgeService = bridgeService;
        }

        /// <summary>
        /// 启动
        /// </summary>
        [HttpPost]
        public string Start(BridgeCommandDto dto)
        {
            Category category = new CategoryService().Query()
                                .FirstOrDefault(cate => cate.NID == dto.CategoryCode);

            WorkflowStructure workflowStructure =
                _abstractService.WorkflowStructureService.Query()
                .FirstOrDefault(e => e.CategoryCode == category.NID && e.Status == 1);

            var model = EmitCore.Convert<BridgeCommandDto, Bridge>(dto);
            string instanceID = WorkflowEngine.Instance.Start(workflowStructure.Resource);
            model.InstanceID = instanceID;
            model.Comment = String.IsNullOrEmpty(model.Comment) ? category.Name : model.Comment;
            model.CreateTime = DateTime.Now;
            CommandBus.Dispatch(new CreateBridge(), model);

            var b = _bridgeService.Query(instanceID);
            WorkflowInstance Instance = WorkflowInstance.GetInstance(instanceID);
            var current = GetCurrent(Instance, model.Creator);
            string serialObject = GetAuditNext(current, model.CategoryCode, b.Creator, b.Name, out string selectTransitionID);

            WorkflowEngine.Instance.Jump(new WorkflowContext()
            {
                Instance = Instance,
                ActorID = model.Creator,
                Message = "提交",
                TransitionID = selectTransitionID,
                Data = Newtonsoft.Json.JsonConvert.DeserializeObject(serialObject),
                Current = current
            });

            return instanceID;
        }

        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns></returns>

        [HttpPost]
        public dynamic Get(ActorCommandDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
            IList<Pending> pendings = _pendingService.GetPending(instance.InstanceID, dto.ActorID);

            if (pendings.Count > 0)
            {
                var current = GetCurrent(instance, dto.ActorID);
                Dictionary<string, object> queryArg = new Dictionary<string, object>
                {
                    { "actorID",dto.ActorID },
                    { "instanceID",dto.ID },
                    { "nodeID",current.NID}
                };

                var pending = _pendingService.Query(queryArg).FirstOrDefault();
                var hasAuth = (current.NodeType == WorkflowNodeCategory.Start && instance.State == WorkflowInstanceState.Running) ?
                    true : instance.State == WorkflowInstanceState.Running && pending != null;
                return new
                {
                    current.NID,
                    current.Name,
                    Category = current.NodeType.ToString(),
                    HasAuth = hasAuth
                };
            }
            else
            {
                return new
                {
                    NID = Guid.NewGuid(),
                    Name = instance.State == WorkflowInstanceState.End ? "结束" : "审批中",
                    Category = instance.State == WorkflowInstanceState.End ?
                    WorkflowNodeCategory.End.ToString() : WorkflowNodeCategory.Fork.ToString(),
                    HasAuth = false
                };
            }
        }

        private Node GetCurrent(WorkflowInstance instance, string actorID)
        {
            IList<Pending> pendings = _pendingService.GetPending(instance.InstanceID, actorID);
            Pending model = pendings.FirstOrDefault();
            var current = instance.Current;
            return (current.Count() > 1) ?
                current.FirstOrDefault(e => e.NID == model.NodeID) :
                current.FirstOrDefault();
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="context"></param>
        [HttpPost]
        public void Jump(PostContextDto context)
        {
            WorkflowInstance Instance = WorkflowInstance.GetInstance(context.InstanceID);
            var current = GetCurrent(Instance, context.ActorID);
            WorkflowEngine.Instance.Jump(new WorkflowContext()
            {
                Instance = Instance,
                ActorID = context.ActorID,
                Message = context.Message,
                TransitionID = context.TransitionID,
                Data = context.Data,
                Current = current
            });
        }


        /// <summary>
        /// 重新发起流程
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        public void Reboot(WorkflowDeleteDto dto)
        {
            WorkflowInstance wfInstance = WorkflowInstance.GetInstance(dto.InstanceID);
            string resourceXml = wfInstance.Resource;
            CommandBus.Dispatch<string>(new DeleteWFRecord(), dto.InstanceID);

            string instanceID = WorkflowEngine.Instance.Start(resourceXml);
            Bridge bridge = _bridgeService.GetBridge(dto.Key);
            bridge.InstanceID = instanceID;
            CommandBus.Dispatch<Bridge>(new UpdateBridge(), bridge);

            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            var current = GetCurrent(instance, bridge.Creator);
            List<Transition> transitions = NodeService.GetExecuteTransition(current);
            Transition transitionSelect = transitions.FirstOrDefault();

            Node nextNode = NodeService.FindNodeByID(transitionSelect.Destination, instanceID);
            var node = NodeService.GetNode(nextNode);

            List<string> groupIDs = new List<string>();
            List<string> actorIDs = new List<string>();
            List<string> carbonIDs = new List<string>();
            List<string> organizationIDs = new List<string>();

            foreach (Group g in node.Groups)
            {
                groupIDs.Add(g.ID.ToString());
            }
            foreach (Carbon c in node.Carbons)
            {
                carbonIDs.Add(c.ID.ToString());
            }
            foreach (Actor item in node.Actors)
            {
                actorIDs.Add(item.ID);
            }
            foreach (Smartflow.Elements.Organization item in node.Organizations)
            {
                organizationIDs.Add(item.ID);
            }

            var serialObject = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                CategoryCode = dto.CategoryCode,
                UUID = bridge.Creator,
                bridge.Name,
                Group = string.Join(",", groupIDs),
                Actor = string.Join(",", actorIDs),
                Carbon = string.Join(",", carbonIDs),
                Organization = string.Join(",", organizationIDs)
            });

            WorkflowEngine.Instance.Jump(new WorkflowContext()
            {
                Instance = instance,
                ActorID = bridge.Creator,
                Message = String.Empty,
                TransitionID = transitionSelect.NID,
                Current = GetCurrent(instance, bridge.Creator),
                Data = Newtonsoft.Json.JsonConvert.DeserializeObject(serialObject)
            });
        }

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="context"></param>
        [HttpPost]
        public void Kill(PostLostContextDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
            Node current = NodeService.FindNodeByID(dto.Destination, dto.ID);
            WorkflowEngine.Instance.Kill(instance, new WorkflowContext()
            {
                Message = dto.Message,
                Instance = instance,
                NodeID = current.NID,
                Data = dto.Data,
                Current = current
            });
        }

        [HttpPost]
        public IEnumerable<Transition> GetTransition(ActorCommandDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
            var current = GetCurrent(instance, dto.ActorID);
            return NodeService.GetExecuteTransition(current);
        }

        private string GetAuditNext(Node current, string categoryCode, string creator, string name, out string selectTransitionID)
        {
            string instanceID = current.InstanceID;
            Transition transitionSelect = current.Transitions.FirstOrDefault();
            Node n = NodeService.FindNodeByID(transitionSelect.Destination, instanceID);
            var node = NodeService.GetNode(n);
            selectTransitionID = transitionSelect.NID;
            List<string> groupIDs = new List<string>();
            List<string> actorIDs = new List<string>();
            List<string> carbonIDs = new List<string>();
            List<string> organizationIDs = new List<string>();
            foreach (Group g in node.Groups)
            {
                groupIDs.Add(g.ID.ToString());
            }
            foreach (Carbon c in node.Carbons)
            {
                carbonIDs.Add(c.ID.ToString());
            }
            foreach (Actor item in node.Actors)
            {
                actorIDs.Add(item.ID);
            }
            foreach (Smartflow.Elements.Organization item in node.Organizations)
            {
                organizationIDs.Add(item.ID);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                CategoryCode = categoryCode,
                UUID = creator,
                Name = name,
                Group = string.Join(",", groupIDs),
                Actor = string.Join(",", actorIDs),
                Carbon = string.Join(",", carbonIDs),
                Organization = string.Join(",", organizationIDs)
            });
        }

        /// <summary>
        ///  获取参与者信息
        /// </summary>
        [HttpPost]
        public dynamic GetActorByNext(RequestInstanceDto dto)
        {
            Transition transition = NodeService.GetNextTransition(dto.Destination, dto.ID);
            Node current = NodeService.FindNodeByID(transition.Destination, dto.ID);
            var node = NodeService.GetNode(current);
            return new
            {
                NodeType = current.NodeType.ToString(),
                Actor = node.Actors,
                Group = node.Groups,
                Organization = node.Organizations,
                Carbon = node.Carbons
            };
        }

        /// <summary>
        ///  获取节点类型
        /// </summary>
        [HttpPost]
        public string GetNodeTypeByNext(RequestInstanceDto dto)
        {
            Transition transition = NodeService.GetNextTransition(dto.Destination, dto.ID);
            Node current = NodeService.FindNodeByID(transition.Destination, dto.ID);
            return current.NodeType.ToString();
        }

        /// <summary>
        /// 获取当前节点是否会签
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns></returns>
        [HttpPost]
        public int GetNodeCooperation(ActorCommandDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);

            Node current = GetCurrent(instance, dto.ActorID);
            return String.IsNullOrEmpty(current.Cooperation)?0:1;
        }

        /// <summary>
        ///  获取参与者信息
        /// </summary>
        [HttpPost]
        public dynamic GetCurrentNodeActor(RequestInstanceDto dto)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(dto.ID);
            Node current = instance.Current.Where(e => e.NID == dto.Destination).FirstOrDefault();
            return new
            {
                NodeType = current.NodeType.ToString(),
                Actor = current.Actors,
                Group = current.Groups,
                Organization = current.Organizations,
                Carbon = current.Carbons
            };
        }
    }
}