using Smartflow.Core.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Common;
using NHibernate;
using NHibernate.Criterion;

namespace Smartflow.Core
{
    public class WorkflowLinkService : IWorkflowLinkService
    {
        public int GetLink(string id, string instanceID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.CreateCriteria(typeof(WorkflowLink))
                .Add(Expression.Eq("InstanceID", instanceID))
                .Add(Expression.Eq("RelationshipID", id))
                .SetProjection(Projections.RowCount()).FutureValue<Int32>().Value;
        }
    }
}
