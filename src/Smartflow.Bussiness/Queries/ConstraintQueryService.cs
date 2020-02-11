using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Smartflow.Bussiness.Models;
using Smartflow.Common;

namespace Smartflow.Bussiness.Queries
{
    public class ConstraintQueryService : IQuery<IList<Constraint>>
    {
        public IList<Constraint> Query()
        {
            return DBUtils.CreateWFConnection()
               .Query<Constraint>(" SELECT * FROM t_constraint ORDER BY Sort ")
               .ToList();
        }
    }
}
