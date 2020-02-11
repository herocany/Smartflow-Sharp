using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class SingleUserByActorQueryService : IQuery<String, string>
    {
        private readonly string SQL_COMMAND_SELECT = @"SELECT OrgCode FROM [dbo].[T_USER] WHERE IDENTIFICATION=@ID";

        public String Query(string id)
        {
            return DBUtils.CreateConnection().ExecuteScalar<String>(SQL_COMMAND_SELECT, new { ID = id });
        }
    }
}
