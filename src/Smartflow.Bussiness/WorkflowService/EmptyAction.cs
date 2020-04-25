using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.WorkflowService
{
    public class EmptyAction : IWorkflowAction
    {
        public void ActionExecute(ExecutingContext executingContext)
        {
           // throw new NotImplementedException();
        }
    }
}
