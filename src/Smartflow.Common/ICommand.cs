using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public interface ICommand
    {
        void Execute(Object o);
    }
}
