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
    public class BackService : IJump
    {
        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        public void Jump(WorkflowContext context)
        {
            Node current = context.Current;

            var to = workflowService.NodeService.GetPrevious(current);
            //var to = workflowService.NodeService.GetNode(previous);
            this.Invoke(to, new ExecutingContext
            {
                From = current,
                To = to,
                Direction = WorkflowOpertaion.Back,
                Instance = context.Instance,
                Data = context.Data,
                Message = context.Message,
                Result = context.Result
            }, context);
        }

        private void Invoke(Node to, ExecutingContext executeContext, WorkflowContext context)
        {
            string instanceID = context.Instance.InstanceID;
            workflowService.InstanceService.Jump(context.Current.ID, to.ID, instanceID, new WorkflowProcess()
            {
                RelationshipID = executeContext.From.NID,
                CreateTime=DateTime.Now,
                ActorID = context.ActorID,
                Origin = executeContext.From.ID,
                Destination = executeContext.To.ID,
                TransitionID = context.TransitionID,
                InstanceID = executeContext.Instance.InstanceID,
                NodeType = executeContext.From.NodeType,
                Direction = WorkflowOpertaion.Back

            }, workflowService.ProcessService);

            workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));

            if (to.NodeType == WorkflowNodeCategory.Decision)
            {
                Jump(new WorkflowContext()
                {
                    Instance = WorkflowInstance.GetInstance(instanceID),
                    TransitionID = context.TransitionID,
                    Data = context.Data,
                    Message = "系统流转",
                    ActorID = context.ActorID,
                    Current = to
                });
            }
        }

        public void Jump(string to, WorkflowContext context)
        {
            this.Jump(context);
        }
    }
}

