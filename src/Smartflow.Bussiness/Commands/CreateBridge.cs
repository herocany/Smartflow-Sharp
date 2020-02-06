using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Dapper;

namespace Smartflow.Bussiness.Commands
{
    public class CreateBridge : ICommand<Bridge>
    {
        private readonly string SQL_COMMAND_INSERT = @"INSERT INTO T_Bridge([InstanceID],[CategoryID],[FormID]) VALUES(@InstanceID,@CategoryID,@FormID)";

        public void Execute(Bridge model)
        {
            DBUtils.CreateWFConnection().Execute(SQL_COMMAND_INSERT, model);
        }
    }
}
