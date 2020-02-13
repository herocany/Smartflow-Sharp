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
    public class SingleUserByActorQueryService : IQuery<String, string>
    {
        public String Query(string id)
        {
            return DBUtils.CreateConnection().ExecuteScalar<String>(ResourceManage.SQL_USER_SELECT_3, new { ID = id });
        }
    }
}
