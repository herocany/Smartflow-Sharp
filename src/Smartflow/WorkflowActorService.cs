using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Elements;
using Smartflow.Internals;
using Dapper;

namespace Smartflow
{

    public class WorkflowActorService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Actor, Action<string, object>>, IWorkflowQuery<IList<Elements.Actor>, string>,IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Actor
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Actor entry, Action<string, object> execute)
        {
            execute(ResourceManage.SQL_WORKFLOW_NODE_ACTOR_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Actor> Query(string instanceID)
        {
            return base.Connection
               .Query<Actor>(ResourceManage.SQL_WORKFLOW_NODE_ACTOR_SELECT,
               new { InstanceID = instanceID })
               .ToList();
        }
    }
}
