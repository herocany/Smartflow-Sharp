using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using Dapper;
namespace Smartflow
{
    public class WorkflowProcessService : WorkflowInfrastructure, IWorkflowProcessService, IWorkflowPersistent<WorkflowProcess, Action<String, object>>, IWorkflowQuery<IList<WorkflowProcess>, Dictionary<string, object>>
    {
        public IList<WorkflowProcess> Get(string instanceID)
        {
            return base.Connection.Query<WorkflowProcess>(ResourceManage.SQL_WORKFLOW_PROCESS_SELECT, new { InstanceID = instanceID }).ToList();
        }

        public void Persistent(WorkflowProcess process, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_PROCESS_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                process.Origin,
                process.Destination,
                process.TransitionID,
                process.InstanceID,
                NodeType = process.NodeType.ToString(),
                process.RelationshipID,
                process.Direction,
                process.ActorID
            });
        }

        public IList<WorkflowProcess> Query(Dictionary<string, object> queryArg)
        {
            return Connection.Query<WorkflowProcess>(ResourceManage.SQL_WORKFLOW_PROCESS_LATEST, new { InstanceID = queryArg["InstanceID"], Direction = queryArg["Direction"].ToString() })
                  .ToList<WorkflowProcess>();
        }

        public IList<dynamic> Query(string instanceID)
        {
            return Connection.Query(ResourceManage.SQL_WORKFLOW_PROCESS_RECORD, new
            {
                InstanceID = instanceID,
                Direction = (int)WorkflowOpertaion.Go
            }).OrderBy(order => order.CreateDateTime).ToList();
        }
    }
}
