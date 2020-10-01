using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Scripts
{
    public partial class ResourceManage
    {
        #region OrganizationService
        public const string SQL_ORGANIZATION_SELECT = @" SELECT  Name,ID,ParentID FROM [dbo].[t_sys_organization] WHERE ParentID=@ID ";
        #endregion

    }
}
