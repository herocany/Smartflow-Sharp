using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowProcessService:IWorkflowPersistent<WorkflowProcess>,IWorkflowQuery<WorkflowProcess>
    {
       IList<dynamic> GetRecords(string instanceID);

       void Detached(WorkflowProcess entry);

       void Update(WorkflowProcess entry);
    }
}
