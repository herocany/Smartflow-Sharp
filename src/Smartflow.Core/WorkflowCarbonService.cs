/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System.Xml.Linq;
using Smartflow.Core.Elements;

namespace Smartflow.Core
{
    public class WorkflowCarbonService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Carbon
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }
    }
}
