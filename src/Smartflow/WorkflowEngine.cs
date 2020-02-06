/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Components;
using Smartflow.Elements;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowEngine
    {
        private readonly static WorkflowEngine singleton = new WorkflowEngine();

        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        protected WorkflowEngine()
        {
        }

        public static WorkflowEngine Instance
        {
            get { return singleton; }
        }

        /// <summary>
        /// 根据传递的流程XML字符串,启动工作流
        /// </summary>
        /// <param name="resourceXml"></param>
        /// <returns></returns>
        public string Start(string resourceXml)
        {
            return workflowService.Start(resourceXml);
        }

        /// <summary>
        /// 进行流程跳转
        /// </summary>
        /// <param name="context"></param>
        public void Jump(WorkflowContext context)
        {
            WorkflowInstance instance = context.Instance;
            if (instance.State == WorkflowInstanceState.Running)
            {
                if (context.TransitionID == Utils.CONST_REJECT_TRANSITION_ID)
                {
                    Reject(instance, context);
                    return;
                }

                Node current = instance.Current;
                Transition currentTransition = current.Transitions
                                  .FirstOrDefault(e => e.NID == context.TransitionID);

                Node to = workflowService.NodeService.Query(current.InstanceID)
                            .Where(e => e.ID == currentTransition.Destination).FirstOrDefault();

                var executeContext = new ExecutingContext()
                {
                    From = current,
                    To = to,
                    Instance = context.Instance,
                    Data = context.Data,
                    Result = false
                };

                if (current.Cooperation == 1)
                {
                    this.Discuss(context, current, executeContext);
                }
                else
                {
                    this.Invoke(context.Instance.InstanceID, currentTransition, executeContext);
                }

                if (to.NodeType == WorkflowNodeCategory.End)
                {
                    workflowService.InstanceService.Transfer(WorkflowInstanceState.End, instance.InstanceID);
                }
                else if (to.NodeType == WorkflowNodeCategory.Decision)
                {
                    Transition transition = (currentTransition.Direction == WorkflowOpertaion.Back) ?
                        workflowService.NodeService.GetBackTransition(WorkflowInstance.GetInstance(instance.InstanceID).Current) :
                        workflowService.NodeService.GetTransition(to);

                    if (transition == null) return;
                    Jump(new WorkflowContext()
                    {
                        Instance = WorkflowInstance.GetInstance(instance.InstanceID),
                        TransitionID = transition.NID,
                        Data = context.Data
                    });
                }
            }
        }

        protected void Invoke(string instanceID, Transition selectTransition, ExecutingContext executeContext)
        {
            workflowService.InstanceService.Jump(selectTransition.Destination, instanceID, new WorkflowProcess()
            {
                RelationshipID = executeContext.From.NID,
                Origin = executeContext.From.ID,
                Destination = executeContext.To.ID,
                TransitionID = selectTransition.NID,
                InstanceID = executeContext.Instance.InstanceID,
                NodeType = executeContext.From.NodeType,
                Direction = selectTransition.Direction
            }, workflowService.ProcessService);

            workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
        }

        protected void Discuss(WorkflowContext context, Node current, ExecutingContext executeContext)
        {
            string instanceID = context.Instance.InstanceID;

            IWorkflowCooperationService workflowCooperationService = workflowService.NodeService.WorkflowCooperationService;
            IStrategyService strategyService = workflowService.StrategyService;

            workflowService.NodeService.WorkflowCooperationService.Persistent(new WorkflowCooperation
            {
                NID = Guid.NewGuid().ToString(),
                NodeID = current.NID,
                InstanceID = context.Instance.InstanceID,
                TransitionID = context.TransitionID,
                CreateDateTime = DateTime.Now
            });

            IList<WorkflowCooperation> records = workflowCooperationService.Query(instanceID)
                                                .Where(e => e.NodeID == current.NID)
                                                .ToList();

            executeContext.Result = strategyService.Check(records);
            if (executeContext.Result)
            {
                string to = strategyService.Use().Execute(records);
                Transition transition = current.Transitions.FirstOrDefault(e => e.NID == to);
                Node node = workflowService.NodeService.Query(current.InstanceID)
                            .Where(e => e.ID == transition.Destination).FirstOrDefault();

                this.Invoke(instanceID, transition, new ExecutingContext()
                {
                    From = current,
                    To = node,
                    Instance = context.Instance,
                    Data = context.Data,
                    Result = executeContext.Result
                });
            }
            else
            {
                workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
            }
        }

        /// <summary>
        /// 流程被驳回
        /// </summary>
        /// <param name="instance">实例</param>
        protected void Reject(WorkflowInstance instance, WorkflowContext context)
        {
            if (instance.State == WorkflowInstanceState.Running)
            {
                workflowService.InstanceService.Transfer(WorkflowInstanceState.Reject, instance.InstanceID);
                WorkflowInstance newInstance = WorkflowInstance.GetInstance(instance.InstanceID);

                workflowService.Actions.ForEach(pluin => pluin.ActionExecute(new ExecutingContext()
                {
                    From = newInstance.Current,
                    To = newInstance.Current,
                    Instance = newInstance,
                    Data = context.Data

                }));
            }
        }
    }
}
