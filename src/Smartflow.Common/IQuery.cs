using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public interface IQuery<out T> where T : class
    {
        T Query();
    }

    public interface IQuery<out T, in S> where T : class
    {
        T Query(S queryArg);
    }


    public interface IPagingQuery<out T, in S> where T : class
    {
        T Query(S queryArg, out int total);
    }


    public interface IQuery
    {
        IList<T> Query<T>(String instanceID);
    }
}
