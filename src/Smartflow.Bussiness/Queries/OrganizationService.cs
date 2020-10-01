using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Scripts;
using Smartflow.Bussiness.Interfaces;

namespace Smartflow.Bussiness.Queries
{
    public class OrganizationService : IOrganizationService
    {
        public IList<Organization> Query(String id)
        {
            return DBUtils.CreateConnection()
               .Query<Organization>(ResourceManage.SQL_ORGANIZATION_SELECT, new { ID = id })
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
