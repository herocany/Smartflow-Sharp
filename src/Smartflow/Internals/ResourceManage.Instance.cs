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
        #region WorkflowInstance
        /// <summary>
        /// 获取工作流实例
        /// </summary>
        public const string SQL_WORKFLOW_INSTANCE = "SELECT X.InstanceID, X.RelationshipID, X.Resource, X.State, X.Mode, Y.Name, Y.NID, Y.ID, Y.NodeType, Y.InstanceID,Y.Cooperation FROM T_INSTANCE X INNER JOIN  T_NODE Y  ON(X.InstanceID = Y.InstanceID AND X.RelationshipID = Y.ID) WHERE X.InstanceID = @InstanceID";

        public const string SQL_WORKFLOW_INSTANCE_INSERT = "INSERT INTO T_INSTANCE(InstanceID,RelationshipID,State,Resource,Mode) VALUES(@InstanceID,@RelationshipID,@State,@Resource,@Mode)";

        public const string SQL_WORKFLOW_INSTANCE_UPDATE_RELATIONSHIP= " UPDATE T_INSTANCE SET RelationshipID=@RelationshipID WHERE InstanceID=@InstanceID ";

        public const string SQL_WORKFLOW_INSTANCE_UPDATE_TRANSFER = " UPDATE T_INSTANCE SET State=@State WHERE InstanceID=@InstanceID ";

        public const string SQL_WORKFLOW_INSTANCE_MODE = " SELECT MODE FROM T_INSTANCE  WHERE InstanceID=@InstanceID  ";
        #endregion
    }
}
