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

        #region WorkflowTransitionService
        public const string SQL_WORKFLOW_TRANSITION_INSERT = "INSERT INTO T_TRANSITION(NID,RelationshipID,Name,Destination,Origin,InstanceID,Expression,ID,Direction) VALUES(@NID,@RelationshipID,@Name,@Destination,@Origin,@InstanceID,@Expression,@ID,@Direction)";
        public const string SQL_WORKFLOW_TRANSITION_SELECT = "SELECT * FROM T_TRANSITION WHERE InstanceID=@InstanceID";
        #endregion

    }
}
