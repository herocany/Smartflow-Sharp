using Smartflow.Core.Components;
using Smartflow.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IWorkflowNodeService : IWorkflowPersistent<Element, Action<object>>, IWorkflowQuery<IList<Node>,string>, IWorkflowParse
    {
        IWorkflowQuery<IList<WorkflowConfiguration>>  ConfigurationService
        {
            get;
        }

        IWorkflowCooperationService WorkflowCooperationService
        {
            get;
        }

         WorkflowTransitionService TransitionService
        {
            get;
            
        }

        Transition GetTransition(Node n);

        IEnumerable<Node> GetNode(string instanceID);

        void Execute(Node entry);

        Node FindNodeByID(string id, string instanceID);

        List<Transition> GetExecuteTransition(Node entry);

        Node GetPrevious(Node entry);

        Transition GetNextTransition(string id, string instanceID);
    }
}
