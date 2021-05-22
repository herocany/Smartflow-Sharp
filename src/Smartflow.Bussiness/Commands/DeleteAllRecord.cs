using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using NHibernate;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Commands
{
    public class DeleteAllRecord : ICommand
    {
        public void Execute(Object o)
        {
            Script script = o as Script;
            using ISession session = DbFactory.OpenSession();
            DbCommand command= session.Connection.CreateCommand();
            command.CommandText = "SMF_DELETE_RECORD";
            command.CommandType = CommandType.StoredProcedure;

            DbParameter instanceIDParameter = command.CreateParameter();
            instanceIDParameter.ParameterName = "InstanceID";
            instanceIDParameter.Value = script.InstanceID;
            instanceIDParameter.DbType =DbType.String;
            instanceIDParameter.Size = 50;
            instanceIDParameter.Direction = ParameterDirection.Input;

            DbParameter keyParameter = command.CreateParameter();

            keyParameter.ParameterName = "Key";
            keyParameter.Value = script.Key;
            keyParameter.DbType = DbType.String;
            keyParameter.Size = 50;
            keyParameter.Direction = ParameterDirection.Input;

            DbParameter categoryCodeParameter = command.CreateParameter();
            categoryCodeParameter.ParameterName = "CategoryCode";
            categoryCodeParameter.Value = script.CategoryCode;
            categoryCodeParameter.DbType = DbType.String;
            categoryCodeParameter.Size = 50;
            categoryCodeParameter.Direction = ParameterDirection.Input;

            command.Parameters.Add(instanceIDParameter);
            command.Parameters.Add(keyParameter);
            command.Parameters.Add(categoryCodeParameter);
            command.ExecuteNonQuery();
        }
    }
}
