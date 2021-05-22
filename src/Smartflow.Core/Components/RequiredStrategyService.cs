using Smartflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smartflow.Core.Components
{
    public class RequiredStrategyService : IStrategyService
    {
        private IWorkflowAssistantService AssistantService
        {
            get { return WorkflowGlobalServiceProvider.Resolve<IWorkflowAssistantService>(); }
        }

        public bool Check(IList<WorkflowCooperation> records)
        {
            if (records.Count > 0)
            {
                var record = records.FirstOrDefault();
                int total = AssistantService.GetAssistant(record.InstanceID, record.NodeID);
                return records.Count == total;
            }
            return false;
        }
    }
}