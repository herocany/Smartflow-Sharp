using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowConfigurationService :WorkflowInfrastructure, IWorkflowQuery<WorkflowConfiguration>
    {
        public IList<WorkflowConfiguration> Query(object condition)
        {
            return base.Connection
                  .Query<WorkflowConfiguration>(" SELECT * FROM T_CONFIG ")
                  .ToList();
        }
    }
}
