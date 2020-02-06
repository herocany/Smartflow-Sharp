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

        #region WorkflowCooperationService

        public const string SQL_WORKFLOW_COOPERATION_INSERT = " INSERT INTO T_cooperation(NID,InstanceID,TransitionID,NodeID,CreateDateTime) VALUES(@NID,@InstanceID,@TransitionID,@NodeID,@CreateDateTime)";
        public const string SQL_WORKFLOW_COOPERATION_DELETE = " DELETE FROM T_cooperation WHERE NodeID=@NodeID AND InstanceID=@InstanceID ";
        public const string SQL_WORKFLOW_COOPERATION_SELECT = " SELECT * FROM T_cooperation  WHERE InstanceID=@InstanceID  ";

        #endregion


    }
}
