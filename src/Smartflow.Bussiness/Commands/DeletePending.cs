using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Commands
{
    public class DeletePending : ICommand<string>, ICommand<Dictionary<string, object>>
    {
        private IDbConnection Connection
        {
            get { return DBUtils.CreateWFConnection(); }
        }

        public void Execute(string instanceID)
        {
            Connection.Execute(ResourceManage.SQL_PENDING_DELETE, new {
                InstanceID = instanceID
            });
        }

        public void Execute(Dictionary<string, object> queryArg)
        {
            Connection.Execute(ResourceManage.SQL_PENDING_DELETE_1, new
            {
                InstanceID = queryArg["instanceID"],
                NodeID = queryArg["nodeID"]
            });
        }
    }
}
