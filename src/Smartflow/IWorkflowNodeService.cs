using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowNodeService : IWorkflowPersistent<Element>, IWorkflowQuery<Node>, IWorkflowParse
    {
        IWorkflowQuery<WorkflowConfiguration> ConfigurationService
        {
            get;
        }

        void DoIncrement(Node node);

        Transition GetTransition(ASTNode n);

        Node GetNode(Node entry);

        List<Transition> GetExecuteTransitions(Node entry);
    }
}
