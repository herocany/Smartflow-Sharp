/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Core.Components;
using Smartflow.Core.Elements;
using Smartflow.Core.Internals;

namespace Smartflow.Core
{
    public sealed class WorkflowEngine
    {
        private static readonly WorkflowEngine singleton = new WorkflowEngine();

        private readonly AbstractWorkflow workflowService = WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>();

        private WorkflowEngine()
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
                var current = context.Current;
                if (!String.IsNullOrEmpty(context.Current.Cooperation))
                {
                    context.TransitionID = CooperationJumpService.Cooperation(context, new ExecutingContext
                    {
                        From = current,
                        To = current,
                        Direction = context.TransitionID == Utils.CONST_BACK_TRANSITION_ID ? WorkflowOpertaion.Back : WorkflowOpertaion.Go,
                        Instance = context.Instance,
                        Data = context.Data,
                        Message = context.Message,
                        Result = context.Result
                    });
                }

                if (!String.IsNullOrWhiteSpace(context.TransitionID))
                {
                    context.Result = true;
                    JumpFactory.Create(context.TransitionID).Jump(context);
                }
            }
        }

        public void Kill(WorkflowInstance instance, WorkflowContext context)
        {
            if (instance.State == WorkflowInstanceState.Running)
            {
                workflowService.InstanceService.Transfer(WorkflowInstanceState.Kill, instance.InstanceID);
                workflowService.Actions.ForEach(pluin => pluin.ActionExecute(new ExecutingContext()
                {
                    From = context.Current,
                    To = context.Current,
                    Instance = WorkflowInstance.GetInstance(instance.InstanceID),
                    Data = context.Data
                }));
            }
        }
    }
}
