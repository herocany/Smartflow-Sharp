using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Interfaces;
using NHibernate;

namespace Smartflow.Bussiness.Queries
{
    public class OrganizationService : IOrganizationService
    {
        public IList<Organization> Query(String id)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                       .Query<Organization>()
                       .Where(e=>e.ParentID==id)
                       .ToList();
        }

        public void Load(string id, IList<Organization> all)
        {
            IList<Organization> orgs = this.Query(id);
            foreach (Organization org in orgs)
            {
                Load(org.ID, all);
                all.Add(org);
            }
        }
    }
}
