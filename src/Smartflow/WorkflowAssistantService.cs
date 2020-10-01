using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowAssistantService : WorkflowInfrastructure, IWorkflowAssistantService
    {
        public int GetAssistant(string instanceID, string nodeID)
        {
            return base.Connection.ExecuteScalar<int>(ResourceManage.SQL_ASSISTANT_SELECT_COUNT, new { NodeID = nodeID, InstanceID = instanceID });
        }
    }
}
