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

        Transition GetTransition(ASTNode n);

        Transition GetBackTransition(Node current);

        Node GetNode(Node entry);

        List<Transition> GetExecuteTransition(WorkflowInstance instance);
    }
}
