/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NHibernate;
using Smartflow.Common;
using Smartflow.Core.Elements;

namespace Smartflow.Core
{

    public class WorkflowActorService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Actor
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }
    }
}
