using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Smartflow.Common;
using Smartflow.Core.Internals;
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
