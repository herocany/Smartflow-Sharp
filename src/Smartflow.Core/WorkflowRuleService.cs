/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using NHibernate;
using Smartflow.Common;
using Smartflow.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Smartflow.Core
{
    public class WorkflowRuleService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Elements.Rule
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

    }
}
