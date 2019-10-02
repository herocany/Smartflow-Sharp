using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class DemocraticStrategy : IWorkflowCooperationStrategy
    {
        public string Decide(IList<WorkflowProcess> records)
        {
            IList<string> selectDestinations = new List<string>();
            foreach (WorkflowProcess workflowProcess in records)
            {
                selectDestinations.Add(workflowProcess.Destination);
            }


            var data = from d in selectDestinations
                       group d by d into g
                       orderby g.Count() descending
                       select g.Key;

            return data.FirstOrDefault();
        }
    }
}
