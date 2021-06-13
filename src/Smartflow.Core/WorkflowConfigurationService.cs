/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Smartflow.Common;

namespace Smartflow.Core
{
    public class WorkflowConfigurationService : IWorkflowQuery<IList<WorkflowConfiguration>>
    {
        public IList<WorkflowConfiguration> Query()
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<WorkflowConfiguration>().ToList();
        }
    }
}
