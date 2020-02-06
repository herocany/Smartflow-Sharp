using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Bussiness.Models;
using Smartflow.Common;
using Dapper;

namespace Smartflow.Bussiness.Queries
{
    public class RecordQueryService : IQuery<IList<Record>, string>
    {
        private readonly string SQL_COMMAND_SELECT = @" SELECT * FROM T_RECORD WHERE InstanceID=@InstanceID  ORDER BY CreateDateTime  ";

        public IList<Record> Query(string instanceID)
        {
            return DBUtils.CreateWFConnection()
                        .Query<Record>(SQL_COMMAND_SELECT, new { InstanceID = instanceID })
                        .ToList();
        }
    }
}
