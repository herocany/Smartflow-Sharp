using Smartflow.Core.Elements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IWorkflowInstanceService : IWorkflowQuery<WorkflowInstance, string>
    {
        void Jump(string origin, string destination, String instanceID,WorkflowProcess process, IWorkflowPersistent<WorkflowProcess, Action<Object>> processService);
   
        string CreateInstance(string nodeID, string resource,Action<object> execute);

        void Transfer(WorkflowInstanceState state, string instanceID);
    }
}
