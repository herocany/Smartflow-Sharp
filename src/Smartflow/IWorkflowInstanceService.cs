using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowInstanceService : IWorkflowQuery<WorkflowInstance>
    {
        void Jump(string transitionTo, String instanceID);

        string CreateInstance(string nodeID, string resource,WorkflowMode mode,Action<string,object> execute);

        void Transfer(WorkflowInstanceState state, string instanceID);
    }
}
