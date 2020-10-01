using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region UserByActorQueryService\UserByNodeQueryService\UserByRoleQueryService\UserByOrganizationQueryService
        public const string SQL_USER_SELECT = @"SELECT ID,OrganizationCode,Name,(SELECT Name FROM[dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName FROM T_SYS_USER WHERE ID IN ({0})";
        public const string SQL_USER_SELECT_1 = @"SELECT ID,OrganizationCode ,Name,(SELECT Name FROM [dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName FROM T_SYS_USER WHERE ID IN (select ActorID from [Smartflow].[dbo].[t_pending] WHERE InstanceID=@InstanceID AND NodeID=@NodeID)";
        public const string SQL_USER_SELECT_2 = @"SELECT ID,OrganizationCode ,Name,(SELECT Name FROM [dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName FROM T_SYS_USER WHERE ID IN (SELECT UID FROM T_SYS_UMR  WHERE RID IN ({0}))";
        public const string SQL_USER_SELECT_3 = @"SELECT OrganizationCode  FROM [dbo].[T_SYS_USER] WHERE ID=@ID";
        public const string SQL_USER_SELECT_4 = @"SELECT ID,OrganizationCode,Name,(SELECT Name FROM [dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName   FROM T_SYS_USER WHERE OrganizationCode IN ({0})";
        public const string SQL_USER_SELECT_5 = @"SELECT ID,OrganizationCode,Name,(SELECT Name FROM [dbo].[t_sys_organization] WHERE ID = OrganizationCode) OrganizationName   FROM T_SYS_USER WHERE ID IN ({0})";
        #endregion
    }
}
