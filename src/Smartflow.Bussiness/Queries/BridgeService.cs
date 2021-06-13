/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class BridgeService : IBridgeService
    {
        public Bridge GetBridge(string id)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Bridge>().Where(r => r.Key == id).ToList().FirstOrDefault();
        }

        public Bridge GetBridgeByInstanceID(string instanceID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Bridge>().Where(r => r.InstanceID == instanceID).ToList().FirstOrDefault();
        }

        public Bridge Query(Dictionary<string, string> queryArg)
        {
            using ISession session = DbFactory.OpenSession();
            return session.CreateCriteria(typeof(Bridge))
                  .Add(Expression.Eq("Key", queryArg["Key"]))
                  .Add(Expression.Eq("CategoryCode", queryArg["CategoryCode"]))
                  .List<Bridge>()
                  .FirstOrDefault();
        }
    }
}
