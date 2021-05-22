using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Core.Elements;

namespace Smartflow.Core.Components
{
    public class VetoService : IJump
    {
        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        public void Jump(WorkflowContext context)
        {
            WorkflowInstance instance = context.Instance;
            if (instance.State == WorkflowInstanceState.Running)
            {
                workflowService.InstanceService.Transfer(WorkflowInstanceState.Reject, instance.InstanceID);
                workflowService.Actions.ForEach(pluin => pluin.ActionExecute(new ExecutingContext()
                {
                    From = context.Current,
                    To = context.Current,
                    Instance = WorkflowInstance.GetInstance(instance.InstanceID),
                    Data = context.Data,
                }));
            }
        }

        public void Jump(string to, WorkflowContext context)
        {
            this.Jump(context);
        }
    }
}
