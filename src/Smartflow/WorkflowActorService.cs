using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Elements;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowActorService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Actor>, IWorkflowQuery<Elements.Actor>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            Actor actor = new Actor();
            actor.Name = element.Attribute("name").Value;
            actor.ID = element.Attribute("id").Value;
            return actor;
        }

        public void Persistent(Actor entry)
        {
            string sql = "INSERT INTO T_ACTOR(NID,ID,RelationshipID,Name,InstanceID) VALUES(@NID,@ID,@RelationshipID,@Name,@InstanceID)";
            base.Connection.Execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),entry.ID,entry.RelationshipID,entry.Name,entry.InstanceID
            });
        }

        public IList<Actor> Query(object condition)
        {
            return base.Connection
               .Query<Actor>(" SELECT * FROM T_ACTOR WHERE InstanceID=@InstanceID ",condition)
               .ToList();
        }
    }
}
