using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using Smartflow.Core.Elements;

namespace Smartflow.Bussiness.Queries
{
    public class PendingService : IPendingService
    {
        public IList<Pending> GetPendingByInstanceID(string id)
        {
            using ISession session = DbFactory.OpenSession();
            return session
                       .Query<Pending>()
                       .Where(e => e.ActorID == id).OrderByDescending(e => e.CreateTime)
                       .ToList();
        }

        public IList<Pending> Query(Dictionary<string, object> queryArg)
        {
            using ISession session = DbFactory.OpenSession();
            return session
                       .CreateCriteria(typeof(Pending))
                       .Add(Expression.Eq("InstanceID", queryArg["instanceID"]))
                       .Add(Expression.Eq("ActorID", queryArg["actorID"]))
                       .Add(Expression.Eq("NodeID", queryArg["nodeID"]))
                       .List<Pending>();
        }

        public IList<Pending> GetPending(string instanceID, string actorID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Pending>()
                    .Where(e => e.InstanceID == instanceID && e.ActorID == actorID)
                    .ToList();
        }
    }
}
