/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.Common;
using Smartflow.Common;
using Dapper;
using System.Collections.Generic;

namespace Smartflow.Internals
{
    internal class DbFactory
    {
        internal static IDbConnection CreateWorkflowConnection()
        {
            return DBUtils.CreateWFConnection();
        }

        internal static IDbConnection CreateConnection(string providerName, string connectionString)
        {
            IDbConnection connection =
                DbProviderFactories.GetFactory(providerName).CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        internal static void Execute(IList<Action<IDbConnection, IDbTransaction>> commands)
        {
            IDbConnection connection = DbFactory.CreateWorkflowConnection();
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (Action<IDbConnection, IDbTransaction> command in commands)
                {
                    command(connection, transaction);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        internal static string Execute(Func<IDbConnection, IDbTransaction,string> callback,IList<Action<IDbConnection, IDbTransaction, string>> commands)
        {
            IDbConnection connection = DbFactory.CreateWorkflowConnection();
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string instanceID= callback(connection, transaction);
                foreach (Action<IDbConnection, IDbTransaction,string> command in commands)
                {
                    command(connection, transaction, instanceID);
                }

                transaction.Commit();
                return instanceID;
            }
            catch
            {
                transaction.Rollback();
                return string.Empty;
            }
        }
    }
}
