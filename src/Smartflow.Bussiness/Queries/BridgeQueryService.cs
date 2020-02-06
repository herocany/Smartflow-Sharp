using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class BridgeQueryService : IQuery<Bridge, string>, IQuery<Bridge, Dictionary<string, string>>
    {
        public Bridge Query(string instanceID)
        {
            return DBUtils.CreateWFConnection()
               .Query<Bridge>(" SELECT * FROM T_Bridge Where InstanceID=@InstanceID ", new
               {
                   InstanceID = instanceID
               }).FirstOrDefault();
        }

        public Bridge Query(Dictionary<string, string> queryArg)
        {
            return DBUtils.CreateWFConnection()
                  .Query<Bridge>(" SELECT * FROM T_Bridge Where FormID=@FormID AND @CategoryID=@CategoryID", new
                  {
                      FormID = queryArg["FormID"],
                      CategoryID = queryArg["CategoryID"]
                  }).FirstOrDefault();
        }
    }
}
