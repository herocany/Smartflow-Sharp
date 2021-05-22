using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Smartflow.Common;
using Smartflow.Core.Internals;

namespace Smartflow.Core
{
    public class WorkflowStructureService : IWorkflowPersistent<WorkflowStructure>,  IWorkflowQuery<IList<WorkflowStructure>>
    {
        public void Persistent(WorkflowStructure entry)
        {
            using ISession session = DbFactory.OpenSession();
            session.SaveOrUpdate(entry);
            session.Flush();
        }

        public void Delete(string id)
        {
            using ISession session = DbFactory.OpenSession();
            var model = session.Get<WorkflowStructure>(id);
            session.Delete(model);
            session.Flush();
        }

        public WorkflowStructure Get(string id)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Get<WorkflowStructure>(id);
        }


        public IList<WorkflowStructure> Query()
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<WorkflowStructure>().OrderByDescending(e => e.CreateTime).ToList();
        }

        public IList<WorkflowStructure> Query(int pageIndex, int pageSize, out int total, Dictionary<string, string> queryArg)
        {
            using ISession session = DbFactory.OpenSession();
            IQueryOver<WorkflowStructure> queries=session.QueryOver<WorkflowStructure>();
            SetQueryArg(queries.RootCriteria, queryArg);
            total = queries.RowCount();
            return queries
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .List();
        }
        
        private void SetQueryArg(ICriteria criteria, Dictionary<string, string> queryArg)
        {
            if (queryArg.ContainsKey("CategoryCode"))
            {
                criteria.Add(Expression.Eq("CategoryCode", queryArg["CategoryCode"]));
            }

            if (queryArg.ContainsKey("Key"))
            {
                criteria.Add(Expression.Like("Name", String.Format("%{0}%", queryArg["Key"])));
            }
        }
    }
}
