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
    public class BridgeService : IBridgeService
    {
        public Bridge GetBridge(string id)
        {
            return DBUtils.CreateWFConnection().Query<Bridge>(ResourceManage.SQL_BRIDGE_SELECT_BY_KEY,
                new { ID = id }).FirstOrDefault();
        }

        public Bridge Query(string instanceID)
        {
            return DBUtils.CreateWFConnection()
               .Query<Bridge>(ResourceManage.SQL_BRIDGE_SELECT_BY_INSTANCEID, new
               {
                   InstanceID = instanceID
               }).FirstOrDefault();
        }

        public Bridge Query(Dictionary<string, string> queryArg)
        {
            return DBUtils.CreateWFConnection()
                  .Query<Bridge>(ResourceManage.SQL_BRIDGE_SELECT, new
                  {
                      Key = queryArg["Key"],
                      CategoryID = queryArg["CategoryID"]
                  }).FirstOrDefault();
        }
    }
}
