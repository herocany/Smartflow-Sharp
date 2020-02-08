using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Queries;

namespace Smartflow.Bussiness.WorkflowService
{
    public class FormAction:IWorkflowAction
    {
        public  void ActionExecute(ExecutingContext executeContext)
        {
            string cateCode = (String)executeContext.Data.CateCode;
            string instanceID = (String)executeContext.Instance.InstanceID;
            FormService.Execute(cateCode, instanceID);
        }
    }
}
