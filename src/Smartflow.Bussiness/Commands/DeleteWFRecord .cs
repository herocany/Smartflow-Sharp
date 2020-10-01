using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Scripts;
using System.Data;

namespace Smartflow.Bussiness.Commands
{
    public class DeleteWFRecord : ICommand<String>
    {
        public void Execute(String id)
        {
            DBUtils.CreateWFConnection().Execute("SMF_DELETE_WF_RECORD",new { InstanceID=id },commandType: CommandType.StoredProcedure);
        }
    }
}
