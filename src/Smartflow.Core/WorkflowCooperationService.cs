using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Proxy;
using Smartflow.Common;
using Smartflow.Core.Internals;

namespace Smartflow.Core
{
    public class WorkflowCooperationService : IWorkflowCooperationService, IWorkflowQuery<List<WorkflowCooperation>, string>, IWorkflowPersistent<WorkflowCooperation>
    {
        public void Persistent(WorkflowCooperation entry)
        {
            using ISession session = DbFactory.OpenSession();
            session.Persist(entry);
            session.Flush();
        }

        public void Delete(string instanceID, string nodeID)
        {
            using ISession session = DbFactory.OpenSession();
            var hql = "Delete From  WorkflowCooperation Where NodeID =:NodeID And InstanceID=:InstanceID ";
            session.CreateQuery(hql)
                 .SetParameter("NodeID", nodeID)
                 .SetParameter("InstanceID", instanceID)
                 .ExecuteUpdate();
            
            session.Flush();
        }

        public List<WorkflowCooperation> Query(string instanceID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<WorkflowCooperation>().Where(e => e.InstanceID == instanceID).ToList();
        }
    }
}
