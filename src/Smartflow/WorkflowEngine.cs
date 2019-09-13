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

                this.Invoke(context, to, transitionTo, (executeContext) => Processing(executeContext));

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
                Increment = executeContext.From.Increment
            });

            workflowService.Actions.ForEach(pluin => pluin.ActionExecute(executeContext));
        }

        protected void Invoke(WorkflowContext context, Node to, string selectTransition, System.Action<ExecutingContext> callback)
        {
            Node current = context.Instance.Current;

            bool validation = true;

            if (workflowService.WorkflowCooperationService != null && current.Cooperation > 0)
            {
                IList<WorkflowProcess> records = workflowService.ProcessService
                            .Query(new { current.InstanceID, current.NID, current.Increment });

                validation = workflowService.WorkflowCooperationService.Check(current, records);
                selectTransition = workflowService.WorkflowCooperationService.SelectStrategy().Decide(records, to.ID);
            }
            if (validation)
            {

                workflowService.InstanceService.Jump(selectTransition, context.Instance.InstanceID);

                var next = WorkflowInstance
                   .GetInstance(current.InstanceID)
                   .Current;
                if (next.NodeType != WorkflowNodeCategory.End)
                {
                    workflowService.NodeService.DoIncrement(next);
                }
            }

            callback(new ExecutingContext()
            {
                From = current,
                To = to,
                TransitionID = context.TransitionID,
                Instance = context.Instance,
                Data = context.Data,
                ActorID = context.ActorID,
                ActorName = context.ActorName,
                IsValid = validation
            });
        }
    }
}
