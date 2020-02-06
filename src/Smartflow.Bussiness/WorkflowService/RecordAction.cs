using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;

namespace Smartflow.Bussiness.WorkflowService
{
    public class RecordAction : IWorkflowAction
    {
        public void ActionExecute(ExecutingContext executeContext)
        {
            if (executeContext.Instance.Current.NodeType != WorkflowNodeCategory.Decision)
            {
                string UUID = (String)executeContext.Data.UUID;
                string auditUserName = (String)executeContext.Data.Name;
                string sign = string.Empty;
                CommandBus.Dispatch<Record>(new CreateRecord(), new Record
                {
                    InstanceID = executeContext.Instance.InstanceID,
                    Name = executeContext.From.Name,
                    Comment = executeContext.Data.Message,
                    CreateDateTime = DateTime.Now,
                    AuditUserID = UUID,
                    Url = sign,
                    AuditUserName = auditUserName,
                    NID = Guid.NewGuid().ToString()
                });
            }
        }
    }
}