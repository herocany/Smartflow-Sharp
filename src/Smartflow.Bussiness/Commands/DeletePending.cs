using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePending : ICommand<string>, ICommand<Dictionary<string, object>>
    {
        private readonly string SQL_COMMAND_DELETE = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID ";
        private readonly string SQL_COMMAND_DELETE_1 = @" DELETE FROM T_PENDING WHERE InstanceID=@InstanceID AND NodeID=@NodeID ";

        private IDbConnection Connection
        {
            get { return DBUtils.CreateWFConnection(); }
        }

        public void Execute(string instanceID)
        {
            Connection.Execute(SQL_COMMAND_DELETE, new {
                InstanceID = instanceID
            });
        }

        public void Execute(Dictionary<string, object> queryArg)
        {
            Connection.Execute(SQL_COMMAND_DELETE_1, new
            {
                InstanceID = queryArg["instanceID"],
                NodeID = queryArg["nodeID"]
            });
        }
    }
}
