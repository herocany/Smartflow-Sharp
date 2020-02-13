using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region ConstraintQueryService
        public const string SQL_CONSTRAINT_SELECT = @"SELECT Identification UniqueId,OrgCode,UserName  FROM T_USER WHERE Identification IN ({0})";
        #endregion
    }
}
