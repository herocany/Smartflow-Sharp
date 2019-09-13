using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public abstract class AbstractWorkflow
    {
        private WorkflowNodeService nodeService = new WorkflowNodeService();

        public List<IWorkflowAction> Actions
        {
            get { return WorkflowGlobalServiceProvider.Query<IWorkflowAction>(); }
        }

        public AbstractWorkflowCooperation WorkflowCooperationService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<AbstractWorkflowCooperation>();
            }
        }

        public WorkflowNodeService NodeService
        {
            get{ return nodeService;}
            set{ nodeService = value;}
        }

        public WorkflowProcessService ProcessService
        {
            get
            {
                return new WorkflowProcessService();
            }
        }

        public WorkflowInstanceService InstanceService
        {
            get
            {
                return new WorkflowInstanceService();
            }
        }

        public abstract string Start(string resourceXml);
    }
}
