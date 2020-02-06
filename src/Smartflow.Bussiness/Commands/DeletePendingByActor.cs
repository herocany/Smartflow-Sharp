using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePendingByActor : ICommand<Dictionary<string, object>>
    {
        private readonly string SQL_COMMAND_DELETE = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID AND NodeID=@NodeID AND ActorID=@ActorID ";

        private IDbConnection Connection
        {
            get { return DBUtils.CreateWFConnection(); }
        }

        public void Execute(Dictionary<string, object> queryArg)
        {
            Connection.Execute(SQL_COMMAND_DELETE, new
            {
                InstanceID = queryArg["instanceID"],
                NodeID = queryArg["nodeID"],
                ActorID = queryArg["actorID"]
            });
        }
    }
}
