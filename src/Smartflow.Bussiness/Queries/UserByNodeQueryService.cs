using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Scripts;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class UserByNodeQueryService : IQuery<IList<User>, Dictionary<string, string>>
    {
     
        public IList<User> Query(Dictionary<string, string> queryArg)
        {
            return DBUtils.CreateConnection()
                .Query<User>(ResourceManage.SQL_USER_SELECT_1, new
                {
                    InstanceID = queryArg["instanceID"],
                    NodeID = queryArg["nodeID"]
                }).ToList();
        }
    }
}
