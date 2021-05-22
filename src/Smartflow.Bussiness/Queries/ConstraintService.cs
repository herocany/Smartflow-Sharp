using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class ConstraintService : IQuery<IList<Constraint>>
    {
        public IList<Constraint> Query()
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Constraint>().ToList();
        }
    }
}
