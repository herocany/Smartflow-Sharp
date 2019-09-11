using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowActionService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Action>, IWorkflowQuery<Elements.Action>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            Elements.Action a = new Elements.Action();
            a.Name = element.Attribute("name").Value;
            a.ID = element.Attribute("id").Value;
            return a;
        }

        public void Persistent(Elements.Action entry)
        {
            string sql = "INSERT INTO T_ACTION(NID,ID,RelationshipID,Name,InstanceID) VALUES(@NID,@ID,@RelationshipID,@Name,@InstanceID)";
            base.Connection.Execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Elements.Action> Query(object condition)
        {
            return base.Connection.Query<Elements.Action>(" SELECT * FROM T_GROUP WHERE InstanceID=@InstanceID ", condition).ToList();
        }
    }
}

