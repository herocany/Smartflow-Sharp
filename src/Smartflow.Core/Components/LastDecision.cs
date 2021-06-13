/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core.Components
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
