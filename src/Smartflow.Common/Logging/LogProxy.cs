/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Common.Logging
{
    public sealed class LogProxy
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(GlobalObjectService.Configuration.GetSection("Logging:Program").Value, "System");

        public static log4net.ILog Instance
        {
            get
            {
                return log;
            }
        }
    }
}
