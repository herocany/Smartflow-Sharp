/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public interface IWorkflowPersistent<T> where T : class
    {
        void Persistent(T entry);
    }

    public interface IWorkflowPersistent<T,S> where T : class
    {
        void Persistent(T entry,S execute);
    }

}
