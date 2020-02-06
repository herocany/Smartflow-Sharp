using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class UserByNodeQueryService : IQuery<IList<User>, Dictionary<string, string>>
    {
        private readonly string SQL_COMMAND_SELECT = @"SELECT Identification UniqueId,OrgCode,UserName  FROM T_USER WHERE Identification IN (select ActorID from [Smartflow].[dbo].[t_pending] WHERE InstanceID=@InstanceID AND NodeID=@NodeID)";

        public IList<User> Query(Dictionary<string, string> queryArg)
        {
            return DBUtils.CreateConnection()
                .Query<User>(SQL_COMMAND_SELECT, new
                {
                    InstanceID = queryArg["instanceID"],
                    NodeID = queryArg["nodeID"]
                }).ToList();
        }
    }
}
