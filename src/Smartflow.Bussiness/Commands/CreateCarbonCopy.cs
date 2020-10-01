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
    public class CreateCarbonCopy : ICommand<CarbonCopy>
    {
        public void Execute(CarbonCopy model)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_CARBONCOPY_INSERT, model);
        }
    }
}
