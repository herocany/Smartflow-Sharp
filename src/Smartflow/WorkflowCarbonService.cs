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

    public class WorkflowCarbonService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Carbon, Action<string, object>>, IWorkflowQuery<IList<Elements.Carbon>, string>,IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Carbon
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Carbon entry, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_NODE_CARBON_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Carbon> Query(string instanceID)
        {
            return base.Connection
               .Query<Carbon>(ResourceManage.SQL_WORKFLOW_NODE_CARBON_SELECT,
               new { InstanceID = instanceID })
               .ToList();
        }
    }
}
