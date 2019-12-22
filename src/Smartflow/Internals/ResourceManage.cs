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
    /// <summary>
    /// 处理比较复杂的SQL语句、异常、工作流信息
    /// </summary>
    internal class ResourceManage
    {

        private static readonly Dictionary<string, string> resourceConfig = new Dictionary<string, string>();

        static ResourceManage()
        {
            resourceConfig.Add("MAIL_URL_EXPRESSION", @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            resourceConfig.Add("SQL_ACTOR_RECORD", "SELECT Y.ID, X.Name, X.CreateDateTime FROM T_ACTOR X, T_NODE Y WHERE X.InstanceID = @InstanceID AND X.RelationshipID = Y.NID ORDER BY CREATEDATETIME ASC");
            resourceConfig.Add("SQL_WORKFLOW_INSTANCE", "SELECT X.InstanceID, X.RelationshipID, X.Resource, X.State, X.Mode, Y.Name, Y.NID, Y.ID, Y.NodeType, Y.InstanceID, Y.Cooperation FROM T_INSTANCE X INNER JOIN  T_NODE Y  ON(X.InstanceID = Y.InstanceID AND X.RelationshipID = Y.ID) WHERE X.InstanceID = @InstanceID");
            resourceConfig.Add("SQL_WORKFLOW_PROCESS_LATEST", "SELECT * FROM T_PROCESS WHERE InstanceID=@InstanceID   AND Command=@Command ORDER BY CreateDateTime DESC");
            resourceConfig.Add("SQL_WORKFLOW_PROCESS_RECORDS", "SELECT Origin,Destination,(SELECT ID FROM T_TRANSITION T WHERE T.NID=X.TransitionID) ID FROM T_PROCESS X WHERE InstanceID=@InstanceID AND Command=0 ORDER BY CreateDateTime ASC");
            resourceConfig.Add("SQL_WORKFLOW_TRANSITION_RECORD", "SELECT [NID],[RELATIONSHIPID],[NAME],[DESTINATION],[ORIGIN],[INSTANCEID],[EXPRESSION],[ID] FROM T_TRANSITION WHERE NID=@NID");
        }


        /// <summary>
        /// 参与过审批操作信息
        /// </summary>
        public const string SQL_ACTOR_RECORD = "SQL_ACTOR_RECORD";

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        public const string SQL_WORKFLOW_INSTANCE = "SQL_WORKFLOW_INSTANCE";

        /// <summary>
        /// 获取最新批次审批过程记录
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_LATEST = "SQL_WORKFLOW_PROCESS_LATEST";

        /// <summary>
        /// 审批过程所有记录
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_RECORD = "SQL_WORKFLOW_PROCESS_RECORDS";

        /// <summary>
        /// 获取路线
        /// </summary>
        public const string SQL_WORKFLOW_TRANSITION_RECORD = "SQL_WORKFLOW_TRANSITION_RECORD";

        /// <summary>
        /// 验证MAIL地址，正则表达式
        /// </summary>
        public const string MAIL_URL_EXPRESSION = "MAIL_URL_EXPRESSION";

        public static string GetString(string key)
        {
            return resourceConfig[key];
        }
    }
}
