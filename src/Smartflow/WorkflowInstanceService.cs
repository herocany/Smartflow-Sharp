using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Elements;
using Smartflow.Internals;
using Dapper;
using System.Data;

namespace Smartflow
{
    public class WorkflowInstanceService : WorkflowInfrastructure, IWorkflowInstanceService, IWorkflowQuery<IList<WorkflowInstance>, string>
    {
        public void Jump(string transitionTo, String instanceID, WorkflowProcess process, IWorkflowPersistent<WorkflowProcess, Action<String, Object>> processService)
        {
            IList<Action<IDbConnection, IDbTransaction>> commands = new List<Action<IDbConnection, IDbTransaction>>
            {
                (connection, transaction) => connection.Execute(ResourceManage.SQL_WORKFLOW_INSTANCE_UPDATE_RELATIONSHIP, new { RelationshipID = transitionTo, InstanceID = instanceID }, transaction),

                (connection, transaction) => processService.Persistent(process, (commad, entry) => connection.Execute(commad, entry, transaction))
            };

            DbFactory.Execute(commands);
        }

        public string CreateInstance(string nodeID, string resource, WorkflowMode mode, Action<string, object> execute)
        {
            string instanceID = Guid.NewGuid().ToString();

            execute(ResourceManage.SQL_WORKFLOW_INSTANCE_INSERT, new
            {
                InstanceID = instanceID,
                RelationshipID = nodeID,
                State = WorkflowInstanceState.Running.ToString(),
                Resource = resource,
                Mode = mode.ToString()
            });

            return instanceID;
        }

        public void Transfer(WorkflowInstanceState state, string instanceID)
        {
            base.Connection.Execute(ResourceManage.SQL_WORKFLOW_INSTANCE_UPDATE_TRANSFER, new
            {
                State = state.ToString(),
                InstanceID = instanceID
            });
        }

        public IList<WorkflowInstance> Query(string instanceID)
        {
            try
            {
                return Connection.Query<WorkflowInstance, Node, WorkflowInstance>(ResourceManage.SQL_WORKFLOW_INSTANCE, (instance, node) =>
                {
                    instance.Current = WorkflowGlobalServiceProvider.Resolve<IWorkflowNodeService>().GetNode(node);
                    return instance;

                }, param: new { InstanceID = instanceID }, splitOn: "Name").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WorkflowMode GetMode(string instanceID)
        {
            string mode = base.Connection.ExecuteScalar<string>(ResourceManage.SQL_WORKFLOW_INSTANCE_MODE, new { InstanceID = instanceID });

            return String.IsNullOrEmpty(mode) ? WorkflowMode.Transition :
                (WorkflowMode)Enum.Parse(typeof(WorkflowMode), mode);
        }
    }
}
