using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using System.Data;
using NHibernate;
using System.Data.Common;

namespace Smartflow.Bussiness.Commands
{
    public class DeleteWFRecord : ICommand
    {
        public void Execute(Object o)
        {
            using ISession session = DbFactory.OpenSession();
            DbCommand command = session.Connection.CreateCommand();
            command.CommandText = "SMF_DELETE_WF_RECORD";
            command.CommandType = CommandType.StoredProcedure;

            DbParameter instanceIDParameter = command.CreateParameter();
            instanceIDParameter.ParameterName = "InstanceID";
            instanceIDParameter.Value = o;
            instanceIDParameter.DbType = DbType.String;
            instanceIDParameter.Size = 50;
            instanceIDParameter.Direction = ParameterDirection.Input;
            command.ExecuteNonQuery();
        }
    }
}
