/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IWorkflowProcessService : IWorkflowPersistent<WorkflowProcess, Action<object>>, IWorkflowQuery<IList<WorkflowProcess>, Dictionary<string, object>>
    {
        dynamic Query(string instanceID);

        IList<WorkflowProcess> Get(string instanceID);
    }
}
