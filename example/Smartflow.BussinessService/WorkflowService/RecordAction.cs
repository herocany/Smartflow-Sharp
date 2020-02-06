using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;
using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.BussinessService.WorkflowService
{
    public class RecordAction : IWorkflowAction
    {
        private readonly RecordService recordService = new RecordService();

        public void ActionExecute(ExecutingContext executeContext)
        {
            if (executeContext.Instance.Current.NodeType != WorkflowNodeCategory.Decision)
            {
                //写入审批记录
                recordService.Insert(new Record()
                {
                    INSTANCEID = executeContext.Instance.InstanceID,
                    NODENAME = executeContext.From.Name,
                    MESSAGE = executeContext.Data.Message
                });
            }
        }
    }
}