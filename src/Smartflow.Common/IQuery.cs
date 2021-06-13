/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;

namespace Smartflow.Common
{
    public interface IQuery<out T> where T : class
    {
        T Query();
    }

    public interface IPagingQuery<out T, in S> where T : class
    {
        T Query(S queryArg, out int total);
    }
}
