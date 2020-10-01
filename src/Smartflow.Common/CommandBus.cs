using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public class CommandBus
    {
        public static void Dispatch<T>(ICommand<T> command, T entry) where T : class
        {
            command.Execute(entry);
        }

        public static void Dispatch(ICommand command, object o)
        {
            command.Execute(o);
        }
    }
}
