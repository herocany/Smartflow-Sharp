using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowGroupService : WorkflowInfrastructure, IWorkflowPersistent<Group, Action<string, object>>, IWorkflowQuery<Group>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Group
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Group entry, Action<string, object> execute)
        {
            string sql = "INSERT INTO T_GROUP(NID,ID,RelationshipID,Name,InstanceID) VALUES(@NID,@ID,@RelationshipID,@Name,@InstanceID)";
            execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Group> Query(object condition)
        {
            return base.Connection
               .Query<Group>(" SELECT * FROM T_GROUP WHERE InstanceID=@InstanceID ", condition).ToList();

        }
    }
}
