using Smartflow.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Internals;
using Dapper;
namespace Smartflow
{
    public class WorkflowGroupService : WorkflowInfrastructure, IWorkflowPersistent<Group,Action<string, object>>, IWorkflowQuery<IList<Elements.Group>, string>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Group
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Group entry, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_NODE_GROUP_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Group> Query(string instanceID)
        {
            return base.Connection
               .Query<Group>(ResourceManage.SQL_WORKFLOW_NODE_GROUP_SELECT, new { InstanceID = instanceID }).ToList();
        }
    }
}
