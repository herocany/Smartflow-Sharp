using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class FirstDiscuss : IWorkflowDiscuss
    {
        public string Execute(IList<WorkflowCooperation> records)
        {
            var record = records.OrderBy(e => e.CreateDateTime).FirstOrDefault();

            return record.TransitionID;
        }
    }
}
