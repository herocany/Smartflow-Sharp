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
using Smartflow.Elements;

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
                Node current = instance.Current;
                string transitionTo = current.Transitions
                                  .FirstOrDefault(e => e.NID == context.TransitionID).Destination;

                Node to = workflowService.NodeService.Query(new { current.InstanceID })
                            .Where(e => e.ID == transitionTo)
                            .FirstOrDefault();

                var executeContext = new ExecutingContext()
                {
                    From = current,
                    To = to,
                    Instance = context.Instance,
                    Data = context.Data
                };

                Processing(executeContext, context.TransitionID, (WorkflowOpertaion)executeContext.From.Cooperation);

                this.Invoke(context, transitionTo, executeContext);

                if (to.NodeType == WorkflowNodeCategory.End)
                {
                    workflowService.InstanceService.Transfer(WorkflowInstanceState.End, instance.InstanceID);
                }
                else if (to.NodeType == WorkflowNodeCategory.Decision)
                {
                    Transition transition = workflowService.NodeService.GetTransition(to);
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

        /// <summary>
        /// 跳转过程处理入库
        /// </summary>
        /// <param name="executeContext">执行上下文</param>
        protected void Processing(ExecutingContext executeContext,string transitionID, WorkflowOpertaion command)
        {
            workflowService.ProcessService.Persistent(new WorkflowProcess()
            {
                RelationshipID = executeContext.From.NID,
                Origin = executeContext.From.ID,
                Destination = executeContext.To.ID,
                TransitionID = transitionID,
                InstanceID = executeContext.Instance.InstanceID,
                NodeType = executeContext.From.NodeType,
                Command = command
            });
        }

        protected void Invoke(WorkflowContext context, string selectTransition, ExecutingContext executeContext)
        {
            Node current = context.Instance.Current;

            bool validation = true;
            AbstractWorkflowCooperation abstractWorkflowCooperation = workflowService.WorkflowCooperationService;
            if (abstractWorkflowCooperation != null && current.Cooperation > 0)
            {
                IList<WorkflowProcess> records = workflowService.ProcessService.Query(new { current.InstanceID, Command = current.Cooperation })
                    .Where(c => c.RelationshipID == current.NID)
                    .ToList();

                validation = abstractWorkflowCooperation.Check(current, records);
                selectTransition = abstractWorkflowCooperation.SelectStrategy().Decide(records);
                if (validation)
                {
                    abstractWorkflowCooperation.Detached(records, selectTransition, (r) => workflowService.ProcessService.Detached(r), u => workflowService.ProcessService.Update(u));
                }
            }

            executeContext.IsValid = validation;

            if (executeContext.IsValid)
            {
                workflowService.InstanceService.Jump(selectTransition, context.Instance.InstanceID);
            }

            workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
        }
        
        /// <summary>
        /// 原路退回，回退到上一节点
        /// </summary>
        /// <param name="context">工作流上下文</param>
        public void Back(WorkflowContext context)
        {
            WorkflowInstance instance = context.Instance;

            if (instance.State == WorkflowInstanceState.Running)
            {
                Node current = instance.Current;
                Node previous = current.Previous;

                if (previous != null)
                {
                    string transitionTo = previous.ID;

                    Node to = workflowService.NodeService.Query(new { current.InstanceID })
                                .Where(e => e.ID == transitionTo)
                                .FirstOrDefault();

                    var executeContext = new ExecutingContext()
                    {
                        From = current,
                        To = to,
                        Instance = instance,
                        Data = context.Data,
                        IsValid=true
                    };

                    Processing(executeContext,String.Empty, WorkflowOpertaion.Back);

                    workflowService.InstanceService.Jump(transitionTo, instance.InstanceID);

                    if (previous.NodeType == WorkflowNodeCategory.Start)
                    {
                        workflowService.ProcessService.DetachedAll(context.Instance.InstanceID);
                    }

                    workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
                }
            }
        }

        /// <summary>
        /// 流程被驳回
        /// </summary>
        /// <param name="instance">实例</param>
        public void Reject(WorkflowInstance instance)
        {
            if (instance.State == WorkflowInstanceState.Running)
            {
                workflowService.InstanceService.Transfer(WorkflowInstanceState.Reject,instance.InstanceID);
            }
        }
    }
}
