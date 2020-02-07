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
    public class CreateRecord : ICommand<Record>
    {
        public void Execute(Record model)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_RECORD_INSERT, model);
        }
    }
}
