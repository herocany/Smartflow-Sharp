using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;

namespace Smartflow.Bussiness.WorkflowService
{
    public class CarbonCopyAction : IWorkflowAction
    {
        private readonly BaseBridgeService bridgeService = new BaseBridgeService();
        public void ActionExecute(ExecutingContext executeContext)
        {
            var current = executeContext.To;
            if (executeContext.Instance.State != WorkflowInstanceState.Kill && current.NodeType != WorkflowNodeCategory.Decision)
            {
                string instanceID = executeContext.Instance.InstanceID;
                List<User> userList = bridgeService.GetCarbonCopies(current,(String)executeContext.Data.Carbon);
                foreach (User user in userList)
                {
                    WriteCarbon(user.ID, current.NID, instanceID);
                }
            }
        }


        /// <summary>
        /// 抄送
        /// </summary>
        /// <param name="actorID">参与者</param>
        /// <param name="executeContext"></param>
        public void WriteCarbon(string actorID,string nodeID,string instanceID)
        {
            CommandBus.Dispatch<CarbonCopy>(new CreateCarbonCopy(), new CarbonCopy
            {
                NID = Guid.NewGuid().ToString(),
                ActorID = actorID,
                InstanceID = instanceID,
                NodeID= nodeID,
                CreateTime = DateTime.Now
            });
        }
    }
}