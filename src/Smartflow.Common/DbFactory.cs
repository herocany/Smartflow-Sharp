/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using NHibernate;
using NHibernate.Cfg;
using Smartflow.Common.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Smartflow.Common
{
    public class DbFactory
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory _bussinessSessionFactory;

        public static ISessionFactory SessionFactory => _sessionFactory ??= new Configuration()
                    .Configure("hibernate.core.cfg.xml").BuildSessionFactory();

        public static ISessionFactory BussinessSessionFactory => _bussinessSessionFactory ??= new Configuration()
                    .Configure("hibernate.cfg.xml").BuildSessionFactory();

        public static ISession OpenSession()
        {
            return SessionFactory
                  .WithOptions()
                  .Interceptor(new CommandInterceptor())
                  .OpenSession();
        }


        public static ISession OpenBussinessSession()
        {
            return BussinessSessionFactory
                  .WithOptions()
                  .Interceptor(new CommandInterceptor())
                  .OpenSession();
        }

        public static string Execute(ISession session, Func<ISession, string> callback, IList<Action<ISession, string>> commands)
        {
            using ITransaction transaction = session.BeginTransaction();
            try
            {
                string instanceID = callback(session);
                foreach (Action<ISession, string> command in commands)
                {
                    command(session, instanceID);
                }

                transaction.Commit();
                return instanceID;
            }
            catch (Exception ex)
            {
                LogProxy.Instance.Error(ex);
                transaction.Rollback();
                return string.Empty;
            }
        }

        public static IDataReader ExecuteReader(ISession session, string commandText)
        {
            DbConnection connection = session.Connection;
            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteReader();
        }

        public static object ExecuteScalar(ISession session, string commandText)
        {
            DbConnection connection = session.Connection;
            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteScalar();
        }


        public static ISessionFactory CreateSessionFactory(string connectionString, string providerName)
        {
            var cfg = new Configuration().Configure();
            IDictionary<string, string> connectionProperies= cfg.Properties;
            if (connectionProperies.ContainsKey("connection.connection_string"))
            {
                connectionProperies["connection.connection_string"] = connectionString;
            }
            else
            {
                connectionProperies.Add("connection.connection_string",connectionString);
            }
            if (connectionProperies.ContainsKey("connection.driver_class"))
            {
                connectionProperies["connection.driver_class"] = providerName;
            }
            else
            {
                connectionProperies.Add("connection.driver_class", providerName);
            }
            return cfg.BuildSessionFactory();
        }


        public static void Execute(ISession session, IList<Action<ISession>> commands)
        {
            using ITransaction transaction = session.BeginTransaction();
            try
            {
                foreach (Action<ISession> command in commands)
                {
                    command(session);
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
}

