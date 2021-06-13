/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using NHibernate;
using NHibernate.SqlCommand;
using Smartflow.Common.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Smartflow.Common
{
    public class CommandInterceptor: EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            string commandText = sql.ToString();
            LogProxy.Instance.Info(commandText);
            return base.OnPrepareStatement(sql);
        }
    }
}
