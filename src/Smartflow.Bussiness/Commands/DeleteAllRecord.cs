using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Commands
{
    public class DeleteAllRecord : ICommand<Script>
    {
        public void Execute(Script arg)
        {
            DBUtils.CreateWFConnection().Execute("SMF_DELETE_RECORD",
                 new { arg.InstanceID, arg.Key, arg.CategoryID },
                 commandType: CommandType.StoredProcedure);
        }
    }
}
