using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Internals;
using Dapper;
using System.Data;

namespace Smartflow
{
    public class WorkflowTransitionService : WorkflowInfrastructure, IWorkflowPersistent<Transition, Action<string, object>>, IWorkflowTransitionService, IWorkflowParse
    {
        public Transition GetTransition(string id)
        {
            return base.Connection.Query<Transition>(ResourceManage.SQL_WORKFLOW_TRANSITION_SELECT_ID, new { NID = id }).FirstOrDefault();
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

        public void Persistent(Transition entry,Action<string,object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_TRANSITION_INSERT, new
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

        public IList<Transition> Query(string instanceID)
        {
            return base.Connection.Query<Transition>(ResourceManage.SQL_WORKFLOW_TRANSITION_SELECT, new { InstanceID = instanceID }).ToList();
        }
    }
}
