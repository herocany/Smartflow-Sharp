using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowInstanceService : IWorkflowQuery<WorkflowInstance, string>
    {
        void Jump(string origin, string destination, String instanceID,WorkflowProcess process, IWorkflowPersistent<WorkflowProcess, Action<String, Object>> processService);
   
        string CreateInstance(string nodeID, string resource,Action<string, object> callback);

        void Transfer(WorkflowInstanceState state, string instanceID);
    }
}
