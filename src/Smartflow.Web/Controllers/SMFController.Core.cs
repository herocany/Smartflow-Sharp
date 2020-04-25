using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;

namespace Smartflow.Web.Controllers
{
    public class SMFController : ApiController
    {
        private readonly AbstractBridgeService _abstractBridgeService;
        private readonly IPendingService _pendingService;
        private readonly IActorService _actorService;
        public SMFController(AbstractBridgeService abstractBridgeService, IPendingService pendingService, IActorService actorService)
        {
            _abstractBridgeService = abstractBridgeService;
            _pendingService = pendingService;
            _actorService = actorService;
        }

        protected IWorkflowNodeService NodeService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>().NodeService;
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        [HttpPost]
        public string Start(string id)
        {
            Category category = new CategoryService().Query()
                                .FirstOrDefault(cate => cate.NID == id);

            WorkflowStructure workflowStructure =
                _abstractBridgeService.WorkflowStructureService.Query()
                .FirstOrDefault(e => e.CateCode == category.NID && e.Status == 1);

            return WorkflowEngine.Instance.Start(workflowStructure.StructXml);
        }

        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="id">实例ID</param>
        /// <returns></returns>
        public dynamic Get(string id, string actorId)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(id);
            var current = instance.Current;

            Dictionary<string, object> queryArg = new Dictionary<string, object>
            {
                { "actorID",actorId },
                { "instanceID",id },
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

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="context"></param>
        [HttpPost]
        public void Jump(PostContext context)
        {
            WorkflowEngine.Instance.Jump(new WorkflowContext()
            {
                Instance = WorkflowInstance.GetInstance(context.InstanceID),
                ActorID=(String)context.Data.UUID,
                TransitionID = (String)context.Data.Transition,
                Data = context.Data
            });
        }

        [HttpGet]
        public IEnumerable<Transition> GetTransition(string id)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(id);
            return NodeService.GetExecuteTransition(instance);
        }

        [HttpGet]
        public IEnumerable<User> GetUser(string id)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(id);
            Dictionary<String, string> queryArg = new Dictionary<string, string>
            {
                { "instanceID", id },
                { "nodeID", instance.Current.NID }
            };

            return _actorService.Query(queryArg);
        }
    }

    public class PostContext
    {
        public string InstanceID
        {
            get;
            set;
        }

        public dynamic Data
        {
            get;
            set;
        }
    }
}