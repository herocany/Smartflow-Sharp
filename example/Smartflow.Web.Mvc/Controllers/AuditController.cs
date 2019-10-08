using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;
using Smartflow.BussinessService.WorkflowService;
using Smartflow.Elements;
using Smartflow.Web.Mvc.Code;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smartflow.Web.Mvc.Controllers
{
    public class AuditController : BaseController
    {
        private readonly RecordService workflowRecordService = new RecordService();
        private readonly BaseWorkflowService bwfs = BaseWorkflowService.Instance;
        private readonly WorkflowDesignService designService = new WorkflowDesignService();

        protected IWorkflowNodeService NodeService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>().NodeService;
            }
        }

        /// <summary>
        /// 审核框架
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="instanceID">实例</param>
        /// <returns></returns>
        public ActionResult AuditFrame(string url, string instanceID)
        {
            ViewBag.renderUrl = (!String.IsNullOrEmpty(instanceID)) ? string.Format("{0}/{1}", url, instanceID) : url;
            ViewBag.url = url;
            ViewBag.instanceID = instanceID;
            return View();
        }

        public ActionResult AuditWindow(string url,string instanceID)
        {

           

            ViewBag.instanceID = instanceID;
            ViewBag.url = url;
            return View();
        }



        public JsonResult Start(string structureID)
        {
            return Json(bwfs.Start(structureID));
        }

        [HttpPost]
        public JsonResult GetTransitions(string instanceID)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            List<Transition> transitions = NodeService.GetExecuteTransitions(instance.Current);
            Node previous = instance.Current.Previous;

            bool support = (previous != null && previous.Cooperation == 0 && instance.Current.Cooperation == 0
                && WorkflowEnvironment.Option == Mode.Mix);

            if (support)
            {
                //会签节点不支持回退
                transitions.Add(new Transition()
                {
                    NID = "back",
                    Name = "原路退回"
                });
            }

            return Json(transitions);
        }

        [HttpPost]
        public JsonResult GetCurrent(string instanceID)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            var current = instance.Current;
            return Json(new
            {
                current.NID,
                current.Name,
                Category = current.NodeType.ToString(),
                HasAuth = (current.Name == "开始" && instance.State == WorkflowInstanceState.Running) ? true :
                    instance.State == WorkflowInstanceState.Running
                    && CommonMethods.CheckAuth(current.NID, instanceID, UserInfo)
            });
        }

        [HttpPost]
        public JsonResult GetRecords(string instanceID)
        {
            return Json(workflowRecordService
                .Query(record => record.INSTANCEID == instanceID));
        }

        /// <summary>
        /// 流程跳转处理接口(请不要直接定义匿名类传递)
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        /// <param name="transitionID">跳转路线ID</param>
        /// <param name="message">审批消息</param>
        /// <param name="action">审批动作（原路退回、跳转）</param>
        /// <returns>是否成功</returns>
        public JsonResult Jump(string instanceID, string transitionID, string url, string message)
        {
            dynamic data = new ExpandoObject();
            data.Message = message;
            data.Url = url;
            data.UserInfo = UserInfo;
            bwfs.Jump(instanceID, transitionID, data);
            return Json(true);
        }


        public JsonResult Reject(string instanceID)
        {
            bwfs.Reject(instanceID);
            return Json(true);
        }

        public JsonResult Back(string instanceID, string url, string message)
        {
            dynamic data = new ExpandoObject();
            data.Message = message;
            data.Url = url;
            data.UserInfo = UserInfo;
            bwfs.Back(instanceID, data);
            return Json(true);
        }


        [HttpPost]
        public JsonResult GetAuditUser(string instanceID)
        {
            WorkflowInstance instance=WorkflowInstance.GetInstance(instanceID);
            return Json(new UserService()
                           .GetPendingUserList(instance.Current.NID, instanceID));
        }
    }
}
