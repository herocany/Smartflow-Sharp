using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowInstanceService : IWorkflowQuery<IList<WorkflowInstance>, string>
    {
        void Jump(string transitionTo, String instanceID,WorkflowProcess process, IWorkflowPersistent<WorkflowProcess, Action<String, Object>> processService);

        string CreateInstance(string nodeID, string resource, WorkflowMode mode, Action<string, object> execute);

        void Transfer(WorkflowInstanceState state, string instanceID);

        WorkflowMode GetMode(String instanceID);
    }
}
