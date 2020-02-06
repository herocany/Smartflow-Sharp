using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Components
{
    public class DemocraticDiscuss: IWorkflowDiscuss
    {
        public string Execute(IList<WorkflowCooperation> records)
        {
            IList<string> selectDestinations = new List<string>();
            foreach (WorkflowCooperation entry in records)
            {
                selectDestinations.Add(entry.TransitionID);
            }


            var data = from d in selectDestinations
                       group d by d into g
                       orderby g.Count() descending
                       select g.Key;

            return data.FirstOrDefault();
        }
    }
}
