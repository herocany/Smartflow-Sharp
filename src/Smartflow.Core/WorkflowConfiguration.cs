using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Smartflow.Core
{
    public class WorkflowConfiguration
    {
        public virtual long ID
        {
            get;
            set;
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public virtual string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 提供访问者
        /// </summary>
        public virtual string ProviderName
        {
            get;
            set;
        }
    }
}
