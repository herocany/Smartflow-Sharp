using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class FirstDecision : IWorkflowCooperationDecision
    {
        public string Execute(IList<WorkflowCooperation> records)
        {
            var record = records.OrderBy(e => e.CreateTime).FirstOrDefault();

            return record.TransitionID;
        }
    }
}
