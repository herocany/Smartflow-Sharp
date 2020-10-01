using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Elements;
using Smartflow.Internals;
using System.Data;
using Dapper;

namespace Smartflow
{
    public class WorkflowInstanceService : WorkflowInfrastructure, IWorkflowInstanceService, IWorkflowQuery<WorkflowInstance,string>
    {
        public void Jump(string origin, string destination, String instanceID, WorkflowProcess process, IWorkflowPersistent<WorkflowProcess, Action<String, Object>> processService)
        {
            IList<Action<IDbConnection, IDbTransaction>> commands = new List<Action<IDbConnection, IDbTransaction>>
            {
                (connection, transaction) => connection.Execute(ResourceManage.SQL_WORKFLOW_LINK_DELETE, new { RelationshipID = origin, InstanceID = instanceID }, transaction),

                (connection, transaction) => connection.Execute(ResourceManage.SQL_WORKFLOW_LINK, new { NID=Guid.NewGuid(), InstanceID = instanceID,RelationshipID = destination }, transaction),

                (connection, transaction) => processService.Persistent(process, (commad, entry) => connection.Execute(commad, entry, transaction))
            };

            DbFactory.Execute(commands);
        }

        public string CreateInstance(string nodeID, string resource, Action<string, object> callback)
        {
            string instanceID = Guid.NewGuid().ToString();
            callback(ResourceManage.SQL_WORKFLOW_INSTANCE_INSERT, new
            {
                InstanceID = instanceID,
                State = WorkflowInstanceState.Running.ToString(),
                Resource = resource
            });

            callback(ResourceManage.SQL_WORKFLOW_LINK, new
            {
                NID = Guid.NewGuid(),
                InstanceID = instanceID,
                RelationshipID = nodeID
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

        public WorkflowInstance Query(string instanceID)
        {
            try
            {
                WorkflowInstance instance = Connection.Query<WorkflowInstance>(ResourceManage.SQL_WORKFLOW_INSTANCE, param: new { InstanceID = instanceID }).FirstOrDefault();
                instance.Current = WorkflowGlobalServiceProvider.Resolve<IWorkflowNodeService>().GetNode(instanceID);
                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
