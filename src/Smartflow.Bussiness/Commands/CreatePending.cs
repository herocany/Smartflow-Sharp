using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Commands
{
    public class CreatePending: ICommand<Pending>
    {
        public void Execute(Pending model)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_PENDING_INSERT, model);
        }
    }
}
