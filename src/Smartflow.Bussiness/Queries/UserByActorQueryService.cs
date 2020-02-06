using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class UserByActorQueryService : IQuery<IList<User>, string>
    {
        private readonly string SQL_COMMAND_SELECT = @"SELECT Identification UniqueId,OrgCode,UserName  FROM T_USER WHERE Identification IN ({0})";

        public IList<User> Query(string id)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(SQL_COMMAND_SELECT, id)).ToList();
        }
    }
}
