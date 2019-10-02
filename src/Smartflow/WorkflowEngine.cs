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

        private AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

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
                    TransitionID = context.TransitionID,
                    Instance = context.Instance,
                    Data = context.Data,
                    ActorID = context.ActorID,
                    ActorName = context.ActorName
                };

                Processing(executeContext);

                this.Invoke(context, to, transitionTo, executeContext);

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
                        ActorID = context.ActorID,
                        Data = context.Data
                    });
                }
            }
        }

        /// <summary>
        /// 跳转过程处理入库
        /// </summary>
        /// <param name="executeContext">执行上下文</param>
        protected void Processing(ExecutingContext executeContext)
        {
            workflowService.ProcessService.Persistent(new WorkflowProcess()
            {
                RelationshipID = executeContext.From.NID,
                Origin = executeContext.From.ID,
                Destination = executeContext.To.ID,
                TransitionID = executeContext.TransitionID,
                InstanceID = executeContext.Instance.InstanceID,
                NodeType = executeContext.From.NodeType,
                Command = executeContext.From.Cooperation
            });
        }

        protected void Invoke(WorkflowContext context, Node to, string selectTransition, ExecutingContext executeContext)
        {
            Node current = context.Instance.Current;

            bool validation = true;
            AbstractWorkflowCooperation abstractWorkflowCooperation = workflowService.WorkflowCooperationService;
            if (abstractWorkflowCooperation != null && current.Cooperation > 0)
            {
                IList<WorkflowProcess> records = workflowService.ProcessService.Query(new { current.InstanceID, current.NID, Command = current.Cooperation });
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
    }
}
