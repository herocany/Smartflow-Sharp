using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Common;
using Smartflow.Bussiness.Models;

namespace Smartflow.Bussiness.Commands
{
    public class CreateRecord : ICommand<Record>
    {
        private readonly string SQL_COMMAND_INSERT = @"INSERT INTO T_RECORD([NID],[Name],[Comment],[InstanceID],[CreateDateTime],[Url],[AuditUserID],[AuditUserName]) 
                                                       VALUES (@NID,@Name,@Comment,@InstanceID,@CreateDateTime,@Url,@AuditUserID,@AuditUserName)";
        public void Execute(Record model)
        {
            DBUtils.CreateWFConnection().Execute(SQL_COMMAND_INSERT, model);
        }
    }
}
