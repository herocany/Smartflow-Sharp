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
using Smartflow.Core.Elements;

namespace Smartflow.Core.Elements
{
    public class Workflow
    {
        private List<Node> nodes = new List<Node>();

        public virtual string InstanceID
        {
            get;
            set;
        }

        /// <summary>
        /// 流程节点
        /// </summary>
        public virtual List<Node> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
    }
}
