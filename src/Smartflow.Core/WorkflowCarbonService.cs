using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Core.Elements;
using NHibernate;
using Smartflow.Common;

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
