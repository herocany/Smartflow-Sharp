using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Elements;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowInstanceService : WorkflowInfrastructure, IWorkflowQuery<WorkflowInstance>
    {
        protected WorkflowNodeService NodeService
        {
            get { return new WorkflowNodeService(); }
        }

        public void Jump(string transitionTo, String instanceID)
        {
            string update = " UPDATE T_INSTANCE SET RelationshipID=@RelationshipID WHERE InstanceID=@InstanceID ";
            Connection.Execute(update, new
            {
                RelationshipID = transitionTo,
                InstanceID = instanceID
            });
        }

        public string CreateInstance(string nodeID, string resource)
        {
            string instanceID = Guid.NewGuid().ToString();
            string sql = "INSERT INTO T_INSTANCE(InstanceID,RelationshipID,State,Resource) VALUES(@InstanceID,@RelationshipID,@State,@Resource)";

            base.Connection.Execute(sql, new
            {
                InstanceID = instanceID,
                RelationshipID = nodeID,
                State = WorkflowInstanceState.Running.ToString(),
                Resource = resource
            });
            return instanceID;
        }

        public void Transfer(WorkflowInstanceState state, string instanceID)
        {
            string update = " UPDATE T_INSTANCE SET State=@State WHERE InstanceID=@InstanceID ";
            base.Connection.Execute(update, new
            {
                State = state.ToString(),
                InstanceID = instanceID
            });
        }

        public IList<WorkflowInstance> Query(object condition)
        {
            string sql = ResourceManage.GetString(ResourceManage.SQL_WORKFLOW_INSTANCE);
            try
            {
                return Connection.Query<WorkflowInstance, Node, WorkflowInstance>(sql, (instance, node) =>
                {
                    instance.Current = NodeService.GetNode(node);
                    return instance;

                }, param: condition, splitOn: "Name").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
