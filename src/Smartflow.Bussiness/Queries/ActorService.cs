using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Smartflow.Common.Logging;

namespace Smartflow.Bussiness.Queries
{
    public class ActorService : IActorService
    {
        public String GetOrganizationCode(string id)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                  .CreateQuery(" select u.OrganizationCode from User u where u.ID=:ID ")
                  .SetParameter("ID", id)
                  .UniqueResult<string>();
        }

        public IList<User> Query(Dictionary<string, string> queryArg)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                  .GetNamedQuery("queryUserByMulitlpeCondition")
                  .SetParameter("InstanceID", queryArg["instanceID"])
                  .SetParameter("NodeID", queryArg["nodeID"]).List<User>();
        }

        public IList<User> GetActorByOrganization(IEnumerable<string> organizationCodes)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                        .GetNamedQuery("queryActorByOrganization")
                        .SetParameterList("OrganizationCodes", organizationCodes).List<User>();
        }

        public IList<User> GetActorByRole(IEnumerable<string> ids)
        {
            using ISession session = DbFactory.OpenBussinessSession();

            NHibernate.IQuery query = session
                        .GetNamedQuery("queryActorByRole");
            IList<User> users = query.SetParameterList("RIDS", ids).List<User>();
            return users;
        }

        public IList<User> GetUserByRoleIDs(IEnumerable<string> ids)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                        .GetNamedQuery("queryMultipleUserByID")
                        .SetParameterList("IDS", ids).List<User>();
        }
        public User GetUserByID(string id)
        {
            using ISession session = DbFactory.OpenBussinessSession();
            return session
                   .GetNamedQuery("queryUserByID")
                   .SetParameter("ID", id).List<User>().FirstOrDefault();
        }
    }
}
