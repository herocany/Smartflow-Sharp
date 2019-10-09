using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    /// <summary>
    /// 定义工作流使用模式
    /// </summary>
    public enum WorkflowMode
    {
        /// <summary>
        /// 以线导航节点之前的流转
        /// </summary>
        Transition,


        /// <summary>
        /// 混合模式（线和代码混合的模式）
        /// </summary>
        Mix
    }
}
