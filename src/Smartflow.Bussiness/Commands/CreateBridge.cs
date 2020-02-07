using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Dapper;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Commands
{
    public class CreateBridge : ICommand<Bridge>
    {
        public void Execute(Bridge model)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_BRIDGE_INSERT, model);
        }
    }
}
