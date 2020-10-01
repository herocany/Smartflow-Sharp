using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Scripts;
using Smartflow.Bussiness.Interfaces;

namespace Smartflow.Bussiness.Queries
{
    public class PendingService : IPendingService
    {
        private IDbConnection Connection
        {
            get { return DBUtils.CreateWFConnection(); }
        }


        public IList<Pending> Query(string id)
        {
            return Connection
                 .Query<Pending>(ResourceManage.SQL_PENDING_SELECT, new { ActorID = id })
                 .ToList();
        }


        public IList<Pending> Query(Dictionary<string, object> queryArg)
        {
            return Connection
                .Query<Pending>(ResourceManage.SQL_PENDING_SELECT_1, new { InstanceID = queryArg["instanceID"], ActorID = queryArg["actorID"], NodeID = queryArg["nodeID"] })
                .ToList();
        }

        public IList<Pending> GetPending(string instanceID, string actorID)
        {
            return Connection
                .Query<Pending>(ResourceManage.SQL_PENDING_SELECT_2, new {
                    InstanceID = instanceID,
                    ActorID = actorID
                })
                .ToList();
        }
    }
}
