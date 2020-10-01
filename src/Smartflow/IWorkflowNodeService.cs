using Smartflow.Components;
using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowNodeService : IWorkflowPersistent<Element, Action<string, object>>, IWorkflowQuery<IList<Node>,string>, IWorkflowParse
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

        Transition GetTransition(ASTNode n);

        Node GetNode(Node entry);

        IEnumerable<Node> GetNode(string instanceID);

        void Execute(Node entry);

        Node FindNodeByID(string id, string instanceID);

        List<Transition> GetExecuteTransition(Node entry);


        Transition GetNextTransition(string id, string instanceID);
    }
}
