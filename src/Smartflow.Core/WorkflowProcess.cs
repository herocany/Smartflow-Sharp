/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Linq;
using System.Text;
using Smartflow.Core.Internals;
using System.Data;
using System.Collections.Generic;

namespace Smartflow.Core
{
    public class WorkflowProcess
    {
        /// <summary>
        /// 外键
        /// </summary>
        public virtual string RelationshipID
        {
            get;
            set;
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string NID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前节点
        /// </summary>
        public virtual string Origin
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转到的节点
        /// </summary>
        public virtual string Destination
        {
            get;
            set;
        }

        /// <summary>
        /// 路线ID
        /// </summary>
        public virtual string TransitionID
        {
            get;
            set;
        }

        /// <summary>
        /// 实例ID
        /// </summary>
        public virtual string InstanceID
        {
            get;
            set;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public virtual WorkflowNodeCategory NodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 命令类型0：决策指令 1：协办决策指令 2：回退指令 3:前进
        /// </summary>
        public virtual WorkflowOpertaion Direction
        {
            get;
            set;
        }

        public virtual string ActorID
        {
            get;
            set;
        }
    }
}
