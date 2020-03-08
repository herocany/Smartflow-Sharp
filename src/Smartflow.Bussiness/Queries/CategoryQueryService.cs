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
    public class CategoryQueryService:IQuery<IList<Category>>
    {
        public IList<Category> Query()
        {
            return DBUtils.CreateWFConnection()
               .Query<Category>(ResourceManage.SQL_CATEGORY_SELECT)
               .ToList();
        }
    }
}
