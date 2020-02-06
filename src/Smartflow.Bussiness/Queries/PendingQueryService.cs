using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Smartflow.Bussiness.Queries
{
    public class PendingQueryService : IQuery<IList<Pending>, string>, IQuery<IList<Pending>, Dictionary<string, object>>
    {
        private readonly string SQL_COMMAND_SELECT = @" SELECT * FROM T_PENDING WHERE ActorID=@ActorID Order by CreateDateTime Desc ";
        private readonly string SQL_COMMAND_SELECT_1 = @" SELECT * FROM T_PENDING WHERE InstanceID=@InstanceID AND ActorID=@ActorID AND NodeID=@NodeID ";

        private IDbConnection Connection
        {
            get { return DBUtils.CreateWFConnection(); }
        }

        public IList<Pending> Query(string id)
        {
            return Connection
                 .Query<Pending>(SQL_COMMAND_SELECT, new { ActorID = id })
                 .ToList();
        }

        public IList<Pending> Query(Dictionary<string, object> queryArg)
        {
            return Connection
                .Query<Pending>(SQL_COMMAND_SELECT_1, new { InstanceID = queryArg["instanceID"], ActorID = queryArg["actorID"], NodeID = queryArg["nodeID"] })
                .ToList();
        }
    }
}
