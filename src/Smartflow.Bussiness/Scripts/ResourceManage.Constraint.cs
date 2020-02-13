using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region ConstraintQueryService
        public const string SQL_CONSTRAINT_SELECT = @"SELECT * FROM t_constraint ORDER BY Sort";
        #endregion
    }
}
