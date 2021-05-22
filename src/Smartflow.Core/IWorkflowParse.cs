using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Smartflow.Core
{
    public interface IWorkflowParse
    {
        Smartflow.Core.Elements.Element Parse(XElement element);
    }
}
