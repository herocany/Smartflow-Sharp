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
    public class WorkflowOrganizationService : WorkflowInfrastructure, IWorkflowPersistent<Organization, Action<string, object>>, IWorkflowQuery<IList<Elements.Organization>, string>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Organization
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Organization entry, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_NODE_ORGANIZATION_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Organization> Query(string instanceID)
        {
            return base.Connection
               .Query<Organization>(ResourceManage.SQL_WORKFLOW_NODE_ORGANIZATION_SELECT, new { InstanceID = instanceID }).ToList();
        }
    }
}
