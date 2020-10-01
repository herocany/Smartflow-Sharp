/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Smartflow.Elements;

namespace Smartflow.Elements
{
    public class Workflow
    {
        public Workflow()
        {
            this.Nodes = new List<Node>();
        }

        /// <summary>
        /// 流程节点
        /// </summary>
        public List<Node> Nodes
        {
            get;
            set;
        }

        //public WorkflowMode Mode
        //{
        //    get;
        //    set;
        //}
    }
}
