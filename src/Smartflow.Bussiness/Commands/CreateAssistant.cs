using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Smartflow.Common;
using Smartflow.Bussiness.Scripts;

namespace ZTT.MES.WF.Commands
{
    public class CreateAssistant : ICommand
    {
        public void Execute(object o)
        {
            DBUtils.CreateWFConnection().Execute(ResourceManage.SQL_ASSISTANT_INSERT, new { InstanceID = o });
        }
    }
}
