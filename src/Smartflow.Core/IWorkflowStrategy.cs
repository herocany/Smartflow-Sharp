using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IWorkflowStrategy
    {
        string Decide(IList<WorkflowCooperation> records);
    }
}
