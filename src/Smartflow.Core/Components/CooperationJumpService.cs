/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Smartflow.Core.Elements;
using Smartflow.Core.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Smartflow.Core.Components
{
    internal class CooperationJumpService
    {
        public static string Cooperation(WorkflowContext context, ExecutingContext executeContext)
        {
            string resultTo = String.Empty;
            Node current = context.Current;
            AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

            IWorkflowCooperationService workflowCooperationService = workflowService.NodeService.WorkflowCooperationService;
            IStrategyService strategyService = (IStrategyService)Utils.CreateInstance(current.Assistant); 
            string instanceID = context.Instance.InstanceID;
            workflowService.NodeService.WorkflowCooperationService.Persistent(new WorkflowCooperation
            {
                NodeID = current.NID,
                InstanceID = context.Instance.InstanceID,
                TransitionID = context.TransitionID,
                CreateTime = DateTime.Now
            });

            IList<WorkflowCooperation> records = workflowCooperationService.Query(instanceID).Where(e => e.NodeID == current.NID).ToList();
            executeContext.Result = strategyService.Check(records);
            if (executeContext.Result)
            {
                IWorkflowCooperationDecision workflowCooperationDecision = (IWorkflowCooperationDecision)Utils.CreateInstance(current.Cooperation);
                resultTo = workflowCooperationDecision.Execute(records);
                workflowService.NodeService.WorkflowCooperationService.Delete(instanceID, current.NID);
            }
            else
            {
                workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
            }
            return resultTo;
        }
    }
}
