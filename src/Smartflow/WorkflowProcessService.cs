using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowProcessService : WorkflowInfrastructure, IWorkflowProcessService, IWorkflowPersistent<WorkflowProcess>, IWorkflowQuery<WorkflowProcess>
    {
        public void Persistent(WorkflowProcess process)
        {
            string sql = "INSERT INTO T_PROCESS(NID,Origin,Destination,TransitionID,InstanceID,NodeType,RelationshipID,Command) VALUES(@NID,@Origin,@Destination,@TransitionID,@InstanceID,@NodeType,@RelationshipID,@Command)";
            Connection.Execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                process.Origin,
                process.Destination,
                process.TransitionID,
                process.InstanceID,
                NodeType = process.NodeType.ToString(),
                process.RelationshipID,
                process.Command
            });
        }


        public IList<dynamic> GetRecords(string instanceID)
        {
            string query = ResourceManage.GetString(ResourceManage.SQL_WORKFLOW_PROCESS_RECORD);
            return Connection.Query(query, new
            {
                InstanceID = instanceID
            }).OrderBy(order => order.CreateDateTime).ToList();
        }


        public IList<WorkflowProcess> Query(object condition)
        {
            string query = ResourceManage.GetString(ResourceManage.SQL_WORKFLOW_PROCESS_LATEST);
            return Connection.Query<WorkflowProcess>(query, condition).ToList<WorkflowProcess>();
        }


        public void Detached(WorkflowProcess entry)
        {
            Connection.Execute(" DELETE FROM T_PROCESS WHERE NID=@NID ", new { entry.NID });
        }


        public void Update(WorkflowProcess entry)
        {
            Connection.Execute(" UPDATE T_PROCESS SET Command=@Command WHERE NID=@NID ", new { Command = 0, entry.NID });
        }
    }
}
