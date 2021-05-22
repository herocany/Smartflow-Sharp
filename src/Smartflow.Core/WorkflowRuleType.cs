using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public enum WorkflowRuleType
    {
        /// <summary>
        /// 按上节点的审批人部门筛选
        /// </summary>
        NODE_SEND_LATEST_USER,

        /// <summary>
        /// 按发起人部门筛选
        /// </summary>
        NODE_SEND_START_USER
    }
}
