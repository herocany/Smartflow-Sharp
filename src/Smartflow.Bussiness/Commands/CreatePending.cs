using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Smartflow.Bussiness.Commands
{
    public class CreatePending: ICommand<Pending>
    {
        private readonly string SQL_COMMAND_INSERT = @"INSERT INTO T_PENDING([NID],[ActorID],[NodeID],[InstanceID],[NodeName],[CateCode],[CateName],[Url],[CreateDateTime],[Title]) VALUES(@NID,@ActorID,@NodeID,@InstanceID,@NodeName,@CateCode,@CateName,@Url,@CreateDateTime,@Title)";

        public void Execute(Pending model)
        {
            DBUtils.CreateWFConnection().Execute(SQL_COMMAND_INSERT, model);
        }
    }
}
