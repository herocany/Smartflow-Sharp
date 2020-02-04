using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowTransitionService : WorkflowInfrastructure, IWorkflowPersistent<Transition, Action<string, object>>, IWorkflowQuery<Transition>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            Transition entry = new Transition();
            entry.Name = element.Attribute("name").Value;
            entry.Destination = element.Attribute("destination").Value;
            entry.ID = element.Attribute("id").Value;
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

        public void Persistent(Transition entry, Action<string, object> execute)
        {
            string sql = "INSERT INTO T_TRANSITION(NID,RelationshipID,Name,Destination,Origin,InstanceID,Expression,ID) VALUES(@NID,@RelationshipID,@Name,@Destination,@Origin,@InstanceID,@Expression,@ID)";
            execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.RelationshipID,
                entry.Name,
                entry.Destination,
                entry.Origin,
                entry.InstanceID,
                entry.Expression,
                entry.ID
            });
        }

        public IList<Transition> Query(object condition)
        {
            return base.Connection.Query<Transition>(" SELECT * FROM T_TRANSITION WHERE InstanceID=@InstanceID ", condition).ToList();
        }
    }
}
