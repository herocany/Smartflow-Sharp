/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Core.Internals
{
    public static class GenericExtensions
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<T>(this ISet<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }

        }

        public static void ForEach<T>(this ISet<T> collection, Action<T> callback)
        {
            foreach (T item in collection)
            {
                callback(item);
            }
        }
    }
}
