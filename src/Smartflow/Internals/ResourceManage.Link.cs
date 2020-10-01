/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Smartflow.Internals
{
    internal partial class ResourceManage
    {
        #region WorkflowLink
        public const string SQL_WORKFLOW_LINK_SELECT = " SELECT COUNT(1) FROM [dbo].[t_link] WHERE InstanceID = @InstanceID AND RelationshipID=@ID";
        #endregion
    }
}
