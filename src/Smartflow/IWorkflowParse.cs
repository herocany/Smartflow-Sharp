using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Smartflow
{
    public interface IWorkflowParse
    {
        Smartflow.Elements.Element Parse(XElement element);
    }
}
