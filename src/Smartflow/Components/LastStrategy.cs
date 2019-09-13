using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class LastStrategy : IWorkflowCooperationStrategy
    {
        public string Decide(IList<WorkflowProcess> records, string destination)
        {
            var record = records.OrderByDescending(e => e.CreateDateTime).FirstOrDefault();

            return String.IsNullOrEmpty(destination) ? record.Destination : destination;
        }
    }
}
