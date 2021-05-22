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

namespace Smartflow.Core.Internals
{
    internal partial class ResourceManage
    {
        /// <summary>
        /// 验证MAIL地址，正则表达式
        /// </summary>
        public const string MAIL_URL_EXPRESSION = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";

        /// <summary>
        /// 审批过程所有记录(流程图使用)
        /// </summary>
        public const string SQL_WORKFLOW_PROCESS_RECORD = "SELECT Origin, Destination,(SELECT ID FROM T_TRANSITION T WHERE T.NID = X.TransitionID) ID FROM T_PROCESS X WHERE InstanceID = @InstanceID AND Direction = @Direction ORDER BY CreateTime ASC";

    }
}
