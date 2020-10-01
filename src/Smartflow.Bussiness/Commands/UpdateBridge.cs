using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Commands
{
    public class UpdateBridge : ICommand<Bridge>
    {
        public void Execute(Bridge model)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_BRIDGE_UPDATE, model);
        }
    }
}
