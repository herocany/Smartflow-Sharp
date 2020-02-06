using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public interface IWorkflowQuery<out T> where T : class
    {
        T Query();
    }

    public interface IWorkflowQuery<out T, in S> where T : class
    {
        T Query(S queryArg);
    }

    public interface IWorkflowQuery
    {
        IList<T> Query<T>(String instanceID);
    }
}
