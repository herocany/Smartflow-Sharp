using Smartflow.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Core.Internals;
using System.Data;
using NHibernate;
using Smartflow.Common;

namespace Smartflow.Core
{
    public class WorkflowTransitionService : IWorkflowTransitionService, IWorkflowParse
    {
        public Transition GetTransition(string id)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Transition>()
                          .Where(e => e.NID == id)
                          .FirstOrDefault();
        }

        public Element Parse(XElement element)
        {
            Transition entry = new Transition
            {
                Name = element.Attribute("name").Value,
                Destination = element.Attribute("destination").Value,
                ID = element.Attribute("id").Value
            };

           
            if (element.HasElements)
            {
                XElement expression = element.Elements("expression").FirstOrDefault();
                if (expression != null)
                {
                    entry.Expression = expression.Value;
                }
            }

            return entry;
        }

        public IList<Transition> Query(string instanceID)
        {
            using ISession session = DbFactory.OpenSession();
            return session.Query<Transition>()
                          .Where(e => e.InstanceID == instanceID)
                          .ToList();
        }
    }
}
