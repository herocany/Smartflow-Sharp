using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class BackSenderService : IJump
    {
        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        public void Jump(WorkflowContext context)
        {
            throw new NotImplementedException();
        }

        public void Jump(string to, WorkflowContext context)
        {
            throw new NotImplementedException();
        }
    }
}
