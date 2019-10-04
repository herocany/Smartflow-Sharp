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

namespace Smartflow.Internals
{
    internal class DbFactory
    {
        internal static IDbConnection CreateWorkflowConnection()
        {
            SmartflowConfiguration config =
                WorkflowGlobalServiceProvider.Resolve<ISmartflowConfigurationService>().GetConfiguration();
            
            return DbFactory.CreateConnection(config.ProviderName, config.ConnectionString);
        }

        internal static IDbConnection CreateConnection(string providerName, string connectionString)
        {
            IDbConnection connection =
                DbProviderFactories.GetFactory(providerName).CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }
    }
}
