using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class LastDecision : IWorkflowCooperationDecision
    {
        public string Execute(IList<WorkflowCooperation> records)
        {
            var record = records.OrderByDescending(e => e.CreateTime).FirstOrDefault();

            return record.TransitionID;
        }
    }
}
