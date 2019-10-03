/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartflow.Internals;
using System.Data;

namespace Smartflow
{
    public class WorkflowProcess
    {
        /// <summary>
        /// 外键
        /// </summary>
        public string RelationshipID
        {
            get;
            set;
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string NID
        {
            get;
            set;
        }

        /// <summary>
        /// 当前节点
        /// </summary>
        public string Origin
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转到的节点
        /// </summary>
        public string Destination
        {
            get;
            set;
        }

        /// <summary>
        /// 路线ID
        /// </summary>
        public string TransitionID
        {
            get;
            set;
        }

        /// <summary>
        /// 实例ID
        /// </summary>
        public string InstanceID
        {
            get;
            set;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public WorkflowNodeCategory NodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// 命令类型0：决策指令 1：协办决策指令 2：回退指令
        /// </summary>
        public WorkflowOpertaion Command
        {
            get;
            set;
        }
    }
}
