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
        private readonly BaseBridgeService baseBridgeService = new BaseBridgeService();

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

                if (!executeContext.Result&&executeContext.Instance.Current.Cooperation==1)
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
                        List<User> userList = GetUserByGroup(current.Groups, current.Actors);
                        foreach (User user in userList)
                        {
                            WritePending(user.UniqueId, executeContext);
                        }

                        string NID = executeContext.Instance.Current.NID;

                        Dictionary<string, object> deleteArg = new Dictionary<string, object>()
                        {
                            { "instanceID",instanceID},
                            { "nodeID",NID }
                        };

                        CommandBus.Dispatch<Dictionary<string, Object>>(new DeletePending(), deleteArg);
                    }
                }
            }
        }

        protected List<User> GetUserByGroup(List<Elements.Group> items, List<Actor> actors)
        {
            List<User> userList = new List<User>();
            List<string> gList = new List<string>();
            List<string> ids = new List<string>();
            foreach (Elements.Group g in items)
            {
                gList.Add(g.ID.ToString());
            }
            foreach (Actor item in actors)
            {
                ids.Add(item.ID);
            }

            if (ids.Count > 0)
            {
                userList.AddRange(new UserByActorQueryService().Query(string.Join(",", ids)));
            }

            if (gList.Count > 0)
            {
                userList.AddRange(new UserByRoleQueryService().Query(string.Join(",", gList)));
            }

            return userList
                .ToLookup(p => p.UniqueId)
                .Select(c => c.First())
                .ToList();
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
                List<User> userList = GetUserByGroup(current.Groups, current.Actors);
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
            Category model = new CategoryQueryService().Query()
                 .FirstOrDefault(cate => cate.NID == cateCode);
            string json = (String)executeContext.Data.Json;

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
                CateName = model.Name,
                Title = Parse(json, model.Expression)
            };

            CommandBus.Dispatch<Pending>(new CreatePending(), entry);
        }

        private string Parse(string json, string expression)
        {
            if (String.IsNullOrEmpty(json) || string.IsNullOrEmpty(expression))
            {
                return string.Empty;
            }

            MatchCollection mc = Regex.Matches(expression, @"\{([^\{^\}]*)\}", RegexOptions.IgnoreCase);
            IList<string> fields = new List<string>();
            foreach (Match m in mc)
            {
                fields.Add(m.Value.Replace("{", "").Replace("}", ""));
            }

            JObject arg = (JObject)JsonConvert.DeserializeObject(json);
            foreach (string field in fields)
            {
                expression = Regex.Replace(expression, @"\{" + field + @"\}", arg.Value<string>(field));
            }
            return expression;
        }

        public Node GetCurrentNode(string instanceID)
        {
            return WorkflowInstance.GetInstance(instanceID).Current;
        }
    }
}
