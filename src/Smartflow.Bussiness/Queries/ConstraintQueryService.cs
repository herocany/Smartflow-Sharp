using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Scripts;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class ConstraintQueryService : IQuery<IList<Constraint>>
    {
        public IList<Constraint> Query()
        {
            return DBUtils.CreateWFConnection()
               .Query<Constraint>(ResourceManage.SQL_CONSTRAINT_SELECT)
               .ToList();
        }
    }
}
