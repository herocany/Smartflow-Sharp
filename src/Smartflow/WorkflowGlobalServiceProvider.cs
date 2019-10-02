/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Smartflow.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public static class WorkflowGlobalServiceProvider
    {
        private static IList<Type> _globalTypeCollection = new List<Type>();

        private static IList<IWorkflowAction> _partCollection = new List<IWorkflowAction>();

        static WorkflowGlobalServiceProvider()
        {
            _globalTypeCollection.Add(typeof(MailService));
            _globalTypeCollection.Add(typeof(WorkflowService));
            _globalTypeCollection.Add(typeof(WorkflowNodeService));
            _globalTypeCollection.Add(typeof(WorkflowProcessService));
            _globalTypeCollection.Add(typeof(WorkflowInstanceService));
            _globalTypeCollection.Add(typeof(DefaultActionService));
        }

        public static void RegisterGlobalService(Type registerType)
        {
            _globalTypeCollection.Add(registerType);
        }

        public static void RegisterPartService(IWorkflowAction action)
        {
            _partCollection.Add(action);
        }

        public static T Resolve<T>()
        {
            Type map = _globalTypeCollection
                      .Where(e => typeof(T).IsAssignableFrom(e))
                      .FirstOrDefault();

            return (T)Smartflow.Internals.Utils.CreateInstance(map);
        }

        /// <summary>
        /// 移除全局服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Remove<T>() where T : class
        {
            _globalTypeCollection
               .Where(o => typeof(T).IsAssignableFrom(o))
               .ToList()
               .ForEach((entry) => _globalTypeCollection.Remove(entry));
        }


        /// <summary>
        /// 查询注册到全局里面的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> Query<T>() where T : class
        {
            List<T> services = new List<T>();
            _globalTypeCollection.Where(o => typeof(T).IsAssignableFrom(o)).ToList().ForEach(s =>
            {
                services.Add((T)Smartflow.Internals.Utils.CreateInstance(s));
            });

            return services;
        }

        public static IList<IWorkflowAction> QueryActions()
        {
            return WorkflowGlobalServiceProvider._partCollection;
        }
    }
}
