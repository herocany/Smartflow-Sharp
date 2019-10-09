/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Elements;
using Smartflow.Internals;
using System.Data;


namespace Smartflow
{
    public class WorkflowInstance
    {
        public WorkflowInstanceState State
        {
            get;
            set;
        }

        public Node Current
        {
            get;
            set;
        }

        public string InstanceID
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }

        public WorkflowMode Mode
        {
            get;
            set;
        }

        public static WorkflowInstance GetInstance(string instanceID)
        {
            return WorkflowGlobalServiceProvider.Resolve<IWorkflowInstanceService>()
                .Query(new { InstanceID = instanceID })
                .FirstOrDefault();
        }
    }
}
