using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Dapper;
using Smartflow.Bussiness.Scripts;

namespace Smartflow.Bussiness.Queries
{
    public class RecordService : IQuery<List<Record>, string>
    {
        public List<Record> Query(string instanceID)
        {
            return DBUtils.CreateWFConnection()
                        .Query<Record>(ResourceManage.SQL_RECORD_SELECT, new { InstanceID = instanceID })
                        .ToList();
        }
    }
}
