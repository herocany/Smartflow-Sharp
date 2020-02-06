using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowProcessService : IWorkflowPersistent<WorkflowProcess, Action<String, object>>, IWorkflowQuery<IList<WorkflowProcess>, Dictionary<string, object>>
    {
        IList<dynamic> Query(string instanceID);
    }
}
