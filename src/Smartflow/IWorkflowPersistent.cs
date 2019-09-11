using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowPersistent<T> where T : class
    {
        void Persistent(T entry);
    }
}
