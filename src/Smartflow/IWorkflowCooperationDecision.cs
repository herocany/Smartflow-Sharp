using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowCooperationDecision
    {
        string Execute(IList<WorkflowCooperation> records);
    }
}
