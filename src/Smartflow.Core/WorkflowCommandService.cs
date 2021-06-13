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
using Smartflow.Core.Internals;

namespace Smartflow.Core
{
    public class WorkflowCommandService : IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return (element.HasElements) ? new Command
            {
                ID = element.Elements("id").FirstOrDefault().Value,
                Text = element.Elements("text").FirstOrDefault().Value
            } : null;
        }
    }
}
