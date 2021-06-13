using Smartflow.Bussiness.Commands;
using Smartflow.Common;
using Smartflow.Core;
using System;
using System.Collections.Generic;

namespace Smartflow.Bussiness.WorkflowService
{
    public class CooperationPending
    {
        public static void Execute(ExecutingContext executeContext)
        {
            string instanceID = executeContext.Instance.InstanceID;
            string nodeID = executeContext.Direction == WorkflowOpertaion.Back ? executeContext.From.NID
                : executeContext.To.NID;

            string actorID = (String)executeContext.Data.UUID;
            Dictionary<string, object> deleteArg = new Dictionary<string, object>()
            {
                { "instanceID",instanceID},
                { "nodeID",nodeID },
                { "actorID",actorID }
            };
            CommandBus.Dispatch(new DeletePendingByActor(), deleteArg);
        }
    }
}
