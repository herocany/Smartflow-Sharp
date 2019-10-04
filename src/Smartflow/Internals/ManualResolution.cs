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
    internal class ManualResolution: IResolution
    {
        public Workflow Parse(string resourceXml)
        {
            XDocument doc = XDocument.Parse(resourceXml);
            List<ASTNode> nodes = new List<ASTNode>();
            List<XElement> elements = doc.Element("workflow").Elements().ToList();
            Workflow instance = new Workflow();

            foreach (XElement element in elements)
            {
                string nodeName = element.Name.LocalName;
                if (ServiceContainer.Contains(nodeName))
                {
                    IWorkflowParse typeMapper =ServiceContainer.Resolve(nodeName) as IWorkflowParse;
                    nodes.Add(typeMapper.Parse(element) as ASTNode);
                }
            }

            instance.Nodes.AddRange(nodes.Cast<Node>().ToList());
            return instance;
        }
    }
}
