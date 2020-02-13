/*
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://github.com/chengderen/Smartflow
*/
using System;
using System.Collections.Generic;
using System.Linq;

using Smartflow;
using Smartflow.Elements;
using System.Dynamic;
using System.Threading.Tasks;

namespace Smartflow.Bussiness.WorkflowService
{
    public class BaseWorkflowService
    {
        private readonly WorkflowEngine workflowEngine = WorkflowEngine.Instance;

        public string Start(string resourceXml)
        {
            return workflowEngine.Start(resourceXml);
        }

        public void Jump(WorkflowInstance instance, string transitionID, String actorID, dynamic data)
        {
            workflowEngine.Jump(new WorkflowContext()
            {
                Instance = instance,
                ActorID = actorID,
                TransitionID = transitionID,
                Data = data
            });
        }
    }
}