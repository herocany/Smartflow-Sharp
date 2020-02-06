using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Common;
using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.WorkflowService
{
    public class CooperationPending
    {
        public static void Execute(ExecutingContext executeContext)
        {
            string instanceID = executeContext.Instance.InstanceID;
            string nodeID = executeContext.Instance.Current.NID;
            string actorID = (String)executeContext.Data.UUID;

            Dictionary<string, object> deleteArg = new Dictionary<string, object>()
            {
                { "instanceID",instanceID},
                { "nodeID",nodeID },
                { "actorID",actorID }
            };

            CommandBus.Dispatch<Dictionary<string, Object>>(new DeletePendingByActor(), deleteArg);
        }
    }
}
