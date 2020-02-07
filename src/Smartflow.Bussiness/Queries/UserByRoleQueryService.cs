using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Queries
{
    public class UserByRoleQueryService : IQuery<IList<User>, string>
    {
        public IList<User> Query(string id)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(ResourceManage.SQL_USER_SELECT_2, id)).ToList();
        }
    }
}
