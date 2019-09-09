using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Dapper;

namespace Smartflow
{
    public class WorkflowProcessService : WorkflowInfrastructure, IWorkProcessPersistent
    {
        public void Persistent(WorkflowProcess process)
        {
            string sql = "INSERT INTO T_PROCESS(NID,Origin,Destination,TransitionID,InstanceID,NodeType,RelationshipID,Increment) VALUES(@NID,@Origin,@Destination,@TransitionID,@InstanceID,@NodeType,@RelationshipID,@Increment)";
            Connection.Execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                Origin = process.Origin,
                Destination = process.Destination,
                TransitionID = process.TransitionID,
                InstanceID = process.InstanceID,
                NodeType = process.NodeType.ToString(),
                RelationshipID = process.RelationshipID,
                Increment = process.Increment
            });
        }

    

        public IList<WorkflowProcess> GetLatestRecords(string instanceID, string NID, int increment)
        {
            string query = ResourceManage.GetString(ResourceManage.SQL_WORKFLOW_PROCESS_LATEST);
            return Connection.Query<WorkflowProcess>(query, new
            {
                InstanceID = instanceID,
                NID = NID,
                Increment = increment
            }).OrderBy(order => order.CreateDateTime).ToList<WorkflowProcess>();
        }

        public IList<dynamic> GetRecords(string instanceID)
        {
            string query = ResourceManage.GetString(ResourceManage.SQL_WORKFLOW_PROCESS_RECORD);
            return Connection.Query(query, new
            {
                InstanceID = instanceID
            }).OrderBy(order => order.CreateDateTime).ToList();
        }
    }
}
