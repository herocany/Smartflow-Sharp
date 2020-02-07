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
    public class UserByActorQueryService : IQuery<IList<User>, string>
    {
        public IList<User> Query(string id)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(ResourceManage.SQL_USER_SELECT, id)).ToList();
        }
    }
}
