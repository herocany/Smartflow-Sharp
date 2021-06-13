/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Smartflow.Core
{
    internal class WorkflowActionFactory
    {
        public static IWorkflowAction Create(string name)
        {
            return WorkflowGlobalServiceProvider.QueryActions()
                      .FirstOrDefault(entry => string.Equals(entry.GetType().FullName, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
