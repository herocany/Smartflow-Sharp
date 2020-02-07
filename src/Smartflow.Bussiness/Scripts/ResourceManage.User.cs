using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region UserByActorQueryService\UserByNodeQueryService\UserByRoleQueryService
        public const string SQL_USER_SELECT = @"SELECT Identification UniqueId,OrgCode,UserName  FROM T_USER WHERE Identification IN ({0})";
        public const string SQL_USER_SELECT_1 = @"SELECT Identification UniqueId,OrgCode,UserName  FROM T_USER WHERE Identification IN (select ActorID from [Smartflow].[dbo].[t_pending] WHERE InstanceID=@InstanceID AND NodeID=@NodeID)";
        public const string SQL_USER_SELECT_2 = @"SELECT Identification UniqueId,OrgCode,UserName FROM T_USER WHERE Identification IN (SELECT UUID FROM T_UMR  WHERE RID IN ({0}))";
        #endregion
    }
}
