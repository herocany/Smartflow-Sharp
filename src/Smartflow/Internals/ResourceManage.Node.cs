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
        #region WorkflowNodeService

        public const string SQL_WORKFLOW_NODE_INSERT = "INSERT INTO T_NODE(NID,ID,Name,NodeType,InstanceID,Cooperation) VALUES(@NID,@ID,@Name,@NodeType,@InstanceID,@Cooperation)";

        public const string SQL_WORKFLOW_NODE_SELECT = "SELECT * FROM T_NODE WHERE  InstanceID=@InstanceID";

        public const string SQL_WORKFLOW_NODE_SELECT_ID = "SELECT * FROM T_NODE WHERE  InstanceID=@InstanceID AND ID=@ID";
        #endregion
    }
}
