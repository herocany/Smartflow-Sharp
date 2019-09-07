using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public class DemocraticStrategy : IWorkflowCooperationStrategy
    {
        public string Decide(IList<WorkflowProcess> records, string destination)
        {
            IList<string> selectDestinations = new List<string>();
            foreach (WorkflowProcess workflowProcess in records)
            {
                selectDestinations.Add(workflowProcess.Destination);
            }

            if (!String.IsNullOrEmpty(destination))
            {
                selectDestinations.Add(destination);
            }

            var data = from d in selectDestinations
                       group d by d into g
                       orderby g.Count() descending
                       select g.Key;

            string groupKey = data.FirstOrDefault();
            return String.IsNullOrEmpty(groupKey) ? destination : groupKey;
        }
    }
}
