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
        #region WorkflowCommandService

        public const string SQL_WORKFLOW_NODE_COMMAND_INSERT = "INSERT INTO T_Command(NID, ID, RelationshipID, Text, InstanceID) VALUES(@NID, @ID, @RelationshipID, @Text, @InstanceID)";

        public const string SQL_WORKFLOW_NODE_COMMAND_SELECT = "SELECT * FROM T_Command WHERE InstanceID=@InstanceID ";

        #endregion
    }
}
