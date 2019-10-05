/*
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://github.com/chengderen/Smartflow
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Smartflow.BussinessService.Models;
using Smartflow;
using Smartflow.Elements;
using System.Dynamic;
using Smartflow.BussinessService.Services;
using System.Threading.Tasks;

namespace Smartflow.BussinessService.WorkflowService
{
    public class BaseWorkflowService
    {
        private static readonly WorkflowEngine context = WorkflowEngine.Instance;

        private readonly static BaseWorkflowService singleton = new BaseWorkflowService();

        public static BaseWorkflowService Instance
        {
            get { return singleton; }
        }
    
        public string Start(string identification)
        {
            WorkflowDesignService workflowDesignService = new WorkflowDesignService();
            WorkflowStructure structure = workflowDesignService.GetWorkflowStructure(identification);
            return context.Start(structure.STRUCTUREXML);
        }

        public void Jump(string instanceID, string transitionID, dynamic data)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            context.Jump(new WorkflowContext()
            {
                Instance = instance,
                TransitionID = transitionID,
                Data = data
            });
        }

        /// <summary>
        /// 原路退回
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="data"></param>
        public void Back(string instanceID, dynamic data)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            context.Back(new WorkflowContext()
            {
                Instance = instance,
                Data = data
            });
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="instanceID"></param>
        public void Reject(string instanceID)
        {
            WorkflowInstance instance = WorkflowInstance.GetInstance(instanceID);
            //一、驳回流程
            context.Reject(instance);
            //二、删除当前节点中所有人待办任务
        }
    }
}