using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Smartflow.Common
{
    public class DBUtils
    {
        public static IDbConnection CreateConnection()
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["demoConnection"];
            IDbConnection connection = 
                DbProviderFactories.GetFactory(connectionStringSettings.ProviderName).CreateConnection();
            connection.ConnectionString = connectionStringSettings.ConnectionString;
            return connection;
        }

        public static IDbConnection CreateWFConnection()
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["smartflowConnection"];
            IDbConnection connection =
                DbProviderFactories.GetFactory(connectionStringSettings.ProviderName).CreateConnection();
            connection.ConnectionString = connectionStringSettings.ConnectionString;
            return connection;
        }
    }
}
