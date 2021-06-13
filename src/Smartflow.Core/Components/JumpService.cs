/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Smartflow.Core.Elements;
using Smartflow.Core.Internals;

namespace Smartflow.Core.Components
{
    public class JumpService : IJump
    {
        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        public void Jump(WorkflowContext context)
        {
            string instanceID = context.Instance.InstanceID;
            Node current = context.Current;
            Transition currentTransition = WorkflowGlobalServiceProvider.Resolve<IWorkflowTransitionService>().GetTransition(context.TransitionID);
            IList<Node> nodes = workflowService.NodeService.Query(instanceID);
            Node to = nodes.FirstOrDefault(e => e.ID == currentTransition.Destination);
            this.Invoke(to, currentTransition, new ExecutingContext
            {
                From = current,
                To = to,
                Direction = WorkflowOpertaion.Go,
                Instance = context.Instance,
                Data = context.Data,
                Message = context.Message,
                Result = context.Result
            }, context);
        }

        private void Invoke(Node to, Transition selectTransition, ExecutingContext executeContext, WorkflowContext context)
        {
            string instanceID = context.Instance.InstanceID;
            string actorID = context.ActorID;
            workflowService.InstanceService.Jump(selectTransition.Origin,selectTransition.Destination, instanceID, new WorkflowProcess()
            {
                RelationshipID = executeContext.From.NID,
                CreateTime=DateTime.Now,
                ActorID = actorID,
                Origin = executeContext.From.ID,
                Destination = executeContext.To.ID,
                TransitionID = selectTransition.NID,
                InstanceID = executeContext.Instance.InstanceID,
                NodeType = executeContext.From.NodeType,
                Direction = WorkflowOpertaion.Go
            }, workflowService.ProcessService);

            workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
            if (to.NodeType == WorkflowNodeCategory.End)
            {
                workflowService.InstanceService.Transfer(WorkflowInstanceState.End, instanceID);
            }
            else if (to.NodeType == WorkflowNodeCategory.Decision)
            {
                Transition transition = workflowService.NodeService.GetTransition(to);
                if (transition == null) return;
                Node nc = workflowService.NodeService.Query(instanceID).Where(e => e.ID == transition.Origin).FirstOrDefault();
                Jump(new WorkflowContext()
                {
                    Instance = WorkflowInstance.GetInstance(instanceID),
                    TransitionID = transition.NID,
                    Data = context.Data,
                    Message = "系统流转",
                    ActorID = context.ActorID,
                    Current = nc
                });
            }
            else if (to.NodeType == WorkflowNodeCategory.Fork)
            {
                foreach (Transition transition in to.Transitions)
                {
                    Jump(new WorkflowContext()
                    {
                        Instance = WorkflowInstance.GetInstance(instanceID),
                        TransitionID = transition.NID,
                        Data = context.Data,
                        Message = "系统流转",
                        ActorID = context.ActorID,
                        Current = executeContext.To
                    });
                }
            }
            else if (to.NodeType == WorkflowNodeCategory.Merge)
            {
                IList<Transition> transitions =
                    WorkflowGlobalServiceProvider.Resolve<IWorkflowTransitionService>().Query(instanceID);
                int tc = transitions.Count(e => e.Destination == to.ID);
                var newInstance = WorkflowInstance.GetInstance(instanceID);
                int mc = WorkflowGlobalServiceProvider.Resolve<IWorkflowLinkService>().GetLink(to.ID, instanceID);
                if (tc == mc)
                {
                    foreach (Transition transition in to.Transitions)
                    {
                        Jump(new WorkflowContext()
                        {
                            Instance = WorkflowInstance.GetInstance(instanceID),
                            TransitionID = transition.NID,
                            Data = context.Data,
                            Message = "系统流转",
                            ActorID = context.ActorID,
                            Current = executeContext.To
                        });
                    }
                }
            }
        }

        public void Jump(string to,  WorkflowContext context)
        {
            context.TransitionID = to;
            this.Jump(context);
        }
    }
}
