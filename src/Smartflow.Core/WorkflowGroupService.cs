/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Smartflow.Core.Elements;
using System.Xml.Linq;

namespace Smartflow.Core
{
    public class WorkflowGroupService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Group
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }
    }
}
