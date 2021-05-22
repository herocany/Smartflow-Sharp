using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public enum WorkflowOpertaion
    {
        /// <summary>
        /// 决策指令
        /// </summary>
        Decide= 0,

        /// <summary>
        /// 协办指令
        /// </summary>
        Cooperation=1,

        /// <summary>
        /// 回退指令
        /// </summary>
        Back=2,

        /// <summary>
        /// 前进指令
        /// </summary>
        Go=3
    }
}
