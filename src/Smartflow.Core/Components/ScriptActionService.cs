using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core.Components
{
    public class ScriptActionService : IWorkflowAction
    {
        public void ActionExecute(ExecutingContext executingContext)
        {
            var current = executingContext.To;
            if (current.NodeType != WorkflowNodeCategory.Decision)
            {
                IWorkflowNodeService workflowNodeService = WorkflowGlobalServiceProvider.Resolve<IWorkflowNodeService>();
                workflowNodeService.Execute(current);
            }
        }
    }
}
