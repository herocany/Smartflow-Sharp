using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class FirstStrategy : IWorkflowCooperationStrategy
    {
        public string Decide(IList<WorkflowProcess> records)
        {
            var record = records.OrderBy(e => e.CreateDateTime).FirstOrDefault();

            return record.Destination;
        }
    }
}
