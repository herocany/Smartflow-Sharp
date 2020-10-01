using Smartflow.Components;
using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public abstract class AbstractWorkflow
    {
        public List<IWorkflowAction> Actions
        {
            get { return WorkflowGlobalServiceProvider.Query<IWorkflowAction>(); }
        }

        public IWorkflowNodeService NodeService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowNodeService>(); }
        }

        public IWorkflowProcessService ProcessService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowProcessService>(); }
        }

        public IWorkflowInstanceService InstanceService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<IWorkflowInstanceService>();
            }
        }

        public abstract string Start(string resourceXml);
    }
}
