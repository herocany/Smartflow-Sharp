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

        #region WorkflowProcessService

        /// <summary>
        /// 获取最新批次审批过程记录
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_LATEST = "SELECT* FROM T_PROCESS WHERE InstanceID=@InstanceID AND Direction=@Direction ORDER BY CreateTime DESC";

        /// <summary>
        /// 审批过程所有记录
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_SELECT = "SELECT * FROM T_PROCESS  WHERE InstanceID = @InstanceID  ORDER BY CreateTime ASC";


        /// <summary>
        /// 审批过程所有记录(流程图使用)
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_RECORD = "SELECT Origin, Destination,(SELECT ID FROM T_TRANSITION T WHERE T.NID = X.TransitionID) ID FROM T_PROCESS X WHERE InstanceID = @InstanceID AND Direction = @Direction ORDER BY CreateTime ASC";

        public const string SQL_WORKFLOW_PROCESS_INSERT = "INSERT INTO T_PROCESS(NID,Origin,Destination,TransitionID,InstanceID,NodeType,RelationshipID,Direction,ActorID) VALUES(@NID,@Origin,@Destination,@TransitionID,@InstanceID,@NodeType,@RelationshipID,@Direction,@ActorID)";
        #endregion
    }
}
