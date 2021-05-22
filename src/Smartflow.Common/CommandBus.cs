using Smartflow.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public class CommandBus
    {
        public static void Dispatch(ICommand command, object o)
        {
            try
            {
                command.Execute(o);
            }
            catch(Exception ex)
            {
                LogProxy.Instance.Error(ex);
            }
        }
    }
}
