using Smartflow.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Smartflow
{
    public class WorkflowLinkService : WorkflowInfrastructure, IWorkflowLinkService
    {
        public int GetLink(string id, string instanceID)
        {
            return base.Connection.ExecuteScalar<int>(ResourceManage.SQL_WORKFLOW_LINK_SELECT, new{
                      InstanceID = instanceID,
                      ID = id
                  });
        }
    }
}
