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

namespace Smartflow.Bussiness.WorkflowService
{
    public class PendingAction : IWorkflowAction
    {
        private readonly BaseBridgeService bridgeService = new BaseBridgeService();

        public void ActionExecute(ExecutingContext executeContext)
        {
            if (executeContext.Instance.State == WorkflowInstanceState.Reject)
            {
                CommandBus.Dispatch<string>(new DeletePending(),
                    executeContext.Instance.InstanceID);
            }
            else
            {
                if (executeContext.Instance.Current.NodeType == WorkflowNodeCategory.Decision)
                {
                    DecisionJump(executeContext);
                }
                else if (!executeContext.Result && executeContext.Instance.Current.Cooperation == 1)
                {
                    CooperationPending.Execute(executeContext);
                }
                else
                {
                    string instanceID = executeContext.Instance.InstanceID;
                    var current = GetCurrentNode(instanceID);
                    if (current.NodeType == WorkflowNodeCategory.End)
                    {
                        CommandBus.Dispatch<string>(new DeletePending(), instanceID);
                    }
                    else
                    {
                        List<User> userList = bridgeService.GetActorByGroup(current,executeContext.Direction);
                        foreach (User user in userList)
                        {
                            WritePending(user.UniqueId, executeContext);
                        }
                        string NID = executeContext.Instance.Current.NID;
                        CommandBus.Dispatch<Dictionary<string, Object>>(new DeletePending(), new Dictionary<string, object>()
                        {
                            { "instanceID",instanceID},
                            { "nodeID",NID }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 多条件跳转
        /// </summary>
        /// <param name="executeContext">执行上下文</param>
        private void DecisionJump(ExecutingContext executeContext)
        {
            string instanceID = executeContext.Instance.InstanceID;
            string NID = executeContext.Instance.Current.NID;
            var current = GetCurrentNode(executeContext.Instance.InstanceID);

            if (current.NodeType != WorkflowNodeCategory.Decision)
            {
                List<User> userList = bridgeService.GetActorByGroup(current,executeContext.Direction);
                foreach (var user in userList)
                {
                    WritePending(user.UniqueId, executeContext);
                }
            }

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
        /// <param name="executeContext"></param>
        public void WritePending(string actorID, ExecutingContext executeContext)
        {
            var node = GetCurrentNode(executeContext.Instance.InstanceID);
            string cateCode = (String)executeContext.Data.CateCode;
            string instanceID = (String)executeContext.Instance.InstanceID;
            Category model = new CategoryService().Query()
                 .FirstOrDefault(cate => cate.NID == cateCode);
       
            Pending entry = new Pending
            {
                NID = Guid.NewGuid().ToString(),
                ActorID = actorID,
                InstanceID = instanceID,
                NodeID = node.NID,
                Url = model.Url,
                CreateDateTime = DateTime.Now,
                NodeName = node.Name,
                CateCode = cateCode,
                CateName = model.Name
            };

            CommandBus.Dispatch<Pending>(new CreatePending(), entry);
        }

        public Node GetCurrentNode(string instanceID)
        {
            return WorkflowInstance.GetInstance(instanceID).Current;
        }
    }
}
