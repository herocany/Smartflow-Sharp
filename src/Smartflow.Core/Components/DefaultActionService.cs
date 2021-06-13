/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Smartflow.Core.Elements;
using System.Collections.Generic;
using System.Linq;

namespace Smartflow.Core.Components
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
            to.Actions.ToList().ForEach(el =>
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
