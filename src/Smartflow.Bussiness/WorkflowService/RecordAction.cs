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
        private readonly BaseBridgeService bridgeService = new BaseBridgeService();
        public void ActionExecute(ExecutingContext executeContext)
        {
            var current = executeContext.From;
            if (executeContext.Instance.State != WorkflowInstanceState.Kill && current.NodeType != WorkflowNodeCategory.Decision&& current.NodeType!=WorkflowNodeCategory.Fork && current.NodeType != WorkflowNodeCategory.Merge)
            {
                string UUID = (String)executeContext.Data.UUID;
                string auditUserName = (String)executeContext.Data.Name;

                CommandBus.Dispatch<Record>(new CreateRecord(), new Record
                {
                    InstanceID = executeContext.Instance.InstanceID,
                    Name = executeContext.From.Name,
                    NodeID = executeContext.From.NID,
                    Comment = executeContext.Message,
                    CreateDateTime = DateTime.Now,
                    AuditUserID = UUID,
                    Url = string.Empty,
                    AuditUserName = auditUserName,
                    NID = Guid.NewGuid().ToString(),

                });
            }
        }
    }
}