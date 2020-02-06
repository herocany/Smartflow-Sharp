using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowCooperationService : WorkflowInfrastructure, IWorkflowCooperationService, IWorkflowQuery<List<WorkflowCooperation>, string>, IWorkflowPersistent<WorkflowCooperation>
    {
        public void Persistent(WorkflowCooperation entry)
        {
            base.Connection.Execute(ResourceManage.SQL_WORKFLOW_COOPERATION_INSERT, entry);
        }

        public void Delete(string instanceID, string nodeID)
        {
            base.Connection.Execute(ResourceManage.SQL_WORKFLOW_COOPERATION_DELETE, new { InstanceID = instanceID, NodeID = nodeID });
        }

        public List<WorkflowCooperation> Query(string instanceID)
        {
            return base.Connection.Query<WorkflowCooperation>
                  (ResourceManage.SQL_WORKFLOW_COOPERATION_SELECT, new { InstanceID = instanceID }).ToList();
        }
    }
}
