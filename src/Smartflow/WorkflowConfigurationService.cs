using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using Dapper;
namespace Smartflow
{
    public class WorkflowConfigurationService :WorkflowInfrastructure, IWorkflowQuery<IList<WorkflowConfiguration>>
    {
        public IList<WorkflowConfiguration> Query()
        {
            return base.Connection
                  .Query<WorkflowConfiguration>(ResourceManage.SQL_WORKFLOW_NODE_CONFIGURATION_SELECT)
                  .ToList();
        }
    }
}
