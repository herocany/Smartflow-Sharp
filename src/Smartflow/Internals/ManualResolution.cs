/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Smartflow;
using Smartflow.Elements;

namespace Smartflow.Internals
{
    internal class ManualResolution : IResolution
    {

        public Workflow Parse(string resourceXml)
        {
            Workflow instance = new Workflow();
            XDocument doc = XDocument.Parse(resourceXml);
            List<ASTNode> nodes = new List<ASTNode>();
            XElement root = doc.Element("workflow");
            List<XElement> elements = root.Elements().ToList();

            if (root.HasAttributes)
            {
                XAttribute attr = root.Attribute("mode");
                instance.Mode = String.IsNullOrEmpty(attr.Value) ? WorkflowMode.Transition :
                    (WorkflowMode)Enum.Parse(typeof(WorkflowMode), attr.Value, true);
            }

            foreach (XElement element in elements)
            {
                string nodeName = element.Name.LocalName;
                if (ServiceContainer.Contains(nodeName))
                {
                    IWorkflowParse typeMapper = ServiceContainer.Resolve(nodeName) as IWorkflowParse;
                    nodes.Add(typeMapper.Parse(element) as ASTNode);
                }
            }

            instance.Nodes.AddRange(nodes.Cast<Node>().ToList());
            return instance;
        }
    }
}
