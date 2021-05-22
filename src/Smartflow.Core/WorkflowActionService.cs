using Smartflow.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NHibernate;
using Smartflow.Common;

namespace Smartflow.Core
{
    public class WorkflowActionService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Elements.Action
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }
    }
}

