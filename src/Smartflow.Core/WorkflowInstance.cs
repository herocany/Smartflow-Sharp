/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Core.Elements;
using Smartflow.Core.Internals;
using System.Data;


namespace Smartflow.Core
{
    public class WorkflowInstance
    {
        public virtual WorkflowInstanceState State
        {
            get;
            set;
        }

        public virtual IEnumerable<Node> Current
        {
            get;
            set;
        }

        public virtual string InstanceID
        {
            get;
            set;
        }

        public virtual string Resource
        {
            get;
            set;
        }

        public static WorkflowInstance GetInstance(string instanceID)
        {
            return WorkflowGlobalServiceProvider.Resolve<IWorkflowInstanceService>()
                .Query(instanceID);
        }
    }
}
