using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class DefaultActionService : IWorkflowAction
    {
        public void ActionExecute(ExecutingContext executingContext)
        {
            IList<IWorkflowAction> actions = GetWorkflowActions(executingContext.To);

            foreach (IWorkflowAction action in actions)
            {
                action.ActionExecute(executingContext);
            }
        }

        private List<IWorkflowAction> GetWorkflowActions(Node to)
        {
            List<IWorkflowAction> partAction = new List<IWorkflowAction>();
            to.Actions.ForEach(el =>
            {
                IWorkflowAction defaultAction = WorkflowActionFactory.Create(el.ID);
                if (defaultAction != null)
                {
                    partAction.Add(defaultAction);
                }
            });
            return partAction;
        }
    }
}
