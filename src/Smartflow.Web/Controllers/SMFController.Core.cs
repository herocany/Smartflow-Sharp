using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;

namespace Smartflow.Web.Controllers
{
    public class SMFController : ApiController
    {
        private readonly BaseBridgeService baseBridgeService = new BaseBridgeService();
        private readonly IQuery<IList<Pending>, Dictionary<string, object>> queryService = new PendingQueryService();

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
        public dynamic Start(string id)
        {
            Category category = new CategoryQueryService().Query()
                                .FirstOrDefault(cate => cate.NID == id);

            WorkflowStructure workflowStructure =
                baseBridgeService.WorkflowStructureService.Query()
                .FirstOrDefault(e => e.CateCode == category.NID && e.Status == 1);

            string instanceID = WorkflowEngine.Instance.Start(workflowStructure.StructXml);

            Node current = WorkflowInstance.GetInstance(instanceID).Current;
            string to = current.Transitions.FirstOrDefault().NID;

            return new
            {
                InstanceID = instanceID,
                To = to
            };
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

            var pending = queryService.Query(queryArg).FirstOrDefault();

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
            UserByNodeQueryService userByNodeQueryService = new UserByNodeQueryService();

            Dictionary<String, string> queryArg = new Dictionary<string, string>
            {
                { "instanceID", id },
                { "nodeID", instance.Current.NID }
            };

            return userByNodeQueryService.Query(queryArg);
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