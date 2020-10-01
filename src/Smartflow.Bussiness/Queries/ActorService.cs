using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Scripts;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class ActorService : IActorService
    {
        public IList<User> Query(string id)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(ResourceManage.SQL_USER_SELECT, id)).ToList();
        }

        public String GetOrganizationCode(string id)
        {
            return DBUtils.CreateConnection().ExecuteScalar<String>(ResourceManage.SQL_USER_SELECT_3, new { ID = id });
        }

        public IList<User> Query(Dictionary<string, string> queryArg)
        {
            return DBUtils.CreateConnection()
                .Query<User>(ResourceManage.SQL_USER_SELECT_1, new
                {
                    InstanceID = queryArg["instanceID"],
                    NodeID = queryArg["nodeID"]
                }).ToList();
        }

        public IList<User> GetActorByOrganization(string organizationCodes)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(ResourceManage.SQL_USER_SELECT_4, organizationCodes)).ToList();
        }

        public IList<User> GetActorByRole(string id)
        {
            return DBUtils.CreateConnection()
                .Query<User>(string.Format(ResourceManage.SQL_USER_SELECT_2, id)).ToList();
        }
    }
}
