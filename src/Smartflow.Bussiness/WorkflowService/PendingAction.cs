using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Common;
using Smartflow.Elements;
using System.Text.RegularExpressions;
using ZTT.MES.WF.Commands;

namespace Smartflow.Bussiness.WorkflowService
{
    public class PendingAction : IWorkflowAction
    {
        private readonly BaseBridgeService bridgeService = new BaseBridgeService();

        public void ActionExecute(ExecutingContext executeContext)
        {
            if (executeContext.Instance.State == WorkflowInstanceState.Reject || executeContext.Instance.State == WorkflowInstanceState.Kill)
            {
                CommandBus.Dispatch<string>(new DeletePending(),
                    executeContext.Instance.InstanceID);
            }
            else
            {
                var current = executeContext.To;
                if (current.NodeType == WorkflowNodeCategory.Decision)
                {
                    DecisionJump(executeContext);
                }
                else
                {
                    if (!executeContext.Result && !String.IsNullOrEmpty(executeContext.From.Cooperation))
                    {
                        CooperationPending.Execute(executeContext);
                    }
                    else
                    {
                        string instanceID = executeContext.Instance.InstanceID;
                        if (current.NodeType == WorkflowNodeCategory.End)
                        {
                            CommandBus.Dispatch<string>(new DeletePending(), instanceID);
                        }
                        else
                        {
                            AssignToPendingUser(executeContext, current);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 分派到人
        /// </summary>
        private void AssignToPendingUser(ExecutingContext executeContext,Node current)
        {
            string instanceID = executeContext.Instance.InstanceID;
            //会签或分支节点取默认参与人者；非会签取用户选择的参与者
            bool result = (!String.IsNullOrEmpty(executeContext.From.Cooperation) || executeContext.From.NodeType == WorkflowNodeCategory.Decision || executeContext.From.NodeType == WorkflowNodeCategory.Fork || executeContext.From.NodeType == WorkflowNodeCategory.Merge);
            List<User> userList = result ?
                bridgeService.GetActorByGroup(current, executeContext.Direction) :
                bridgeService.GetActorByGroup((String)executeContext.Data.Actor, (String)executeContext.Data.Group, (String)executeContext.Data.Organization, current, executeContext.Direction);

            CommandBus.Dispatch<Dictionary<string, Object>>(new DeletePending(), new Dictionary<string, object>(){
                { "instanceID",instanceID},
                { "nodeID", executeContext.From.NID }
            });

            foreach (User user in userList)
            {
                WritePending(user.ID, executeContext);
            }

            CommandBus.Dispatch(new CreateAssistant(), instanceID);
        }


        /// <summary>
        /// 多条件跳转
        /// </summary>
        /// <param name="executeContext">执行上下文</param>
        private void DecisionJump(ExecutingContext executeContext)
        {
            string instanceID = executeContext.Instance.InstanceID;
            string NID = executeContext.From.NID;
            Dictionary<string, object> deleteArg = new Dictionary<string, object>()
            {
                { "instanceID",instanceID},
                { "nodeID",NID }
            };
            CommandBus.Dispatch<Dictionary<string, Object>>(new DeletePending(), deleteArg);
        }

        /// <summary>
        /// 写待办信息
        /// </summary>
        /// <param name="actorID">参与者</param>
        /// <param name="executeContext">执行上下文</param>
        public void WritePending(string actorID, ExecutingContext executeContext)
        {
            var node = executeContext.To;
            string categoryCode = (String)executeContext.Data.CategoryCode;
            string instanceID = (String)executeContext.Instance.InstanceID;
            Category model = new CategoryService().Query()
                 .FirstOrDefault(cate => cate.NID == categoryCode);

            Pending entry = new Pending
            {
                NID = Guid.NewGuid().ToString(),
                ActorID = actorID,
                InstanceID = instanceID,
                NodeID = node.NID,
                Url = model.Url,
                CreateTime = DateTime.Now,
                NodeName = node.Name,
                CategoryCode = categoryCode,
                CategoryName = model.Name
            };

            CommandBus.Dispatch<Pending>(new CreatePending(), entry);
        }
    }
}
