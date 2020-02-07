using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePendingByActor : ICommand<Dictionary<string, object>>
    {
        public void Execute(Dictionary<string, object> queryArg)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_PENDING_DELETE_2, new
            {
                InstanceID = queryArg["instanceID"],
                NodeID = queryArg["nodeID"],
                ActorID = queryArg["actorID"]
            });
        }
    }
}
