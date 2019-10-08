using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public static class WorkflowEnvironment
    {
        /// <summary>
        /// 默认以线导航
        /// </summary>
        private static Mode option= Mode.Transition;

        public static Mode Option
        {
            get { return option; }
            set { option = value; }
        }
    }

    /// <summary>
    /// 定义工作流使用模式
    /// </summary>
    public enum Mode
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
