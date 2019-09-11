using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowQuery<T> where T : class
    {
        IList<T> Query(Object condition);
    }
}
