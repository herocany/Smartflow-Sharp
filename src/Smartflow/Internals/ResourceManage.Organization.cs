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
        #region WorkflowOrganizationService

        public const string SQL_WORKFLOW_NODE_ORGANIZATION_INSERT = "INSERT INTO T_ORGANIZATION(NID,ID,RelationshipID,Name,InstanceID) VALUES(@NID,@ID,@RelationshipID,@Name,@InstanceID)";

        public const string SQL_WORKFLOW_NODE_ORGANIZATION_SELECT = "SELECT * FROM T_ORGANIZATION WHERE InstanceID=@InstanceID  ";

        #endregion
    }
}
