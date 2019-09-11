using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow
{
    public class WorkflowConfiguration
    {
        public long ID
        {
            get;
            set;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 提供访问者
        /// </summary>
        public string ProviderName
        {
            get;
            set;
        }
    }
}
