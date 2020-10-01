using Dapper;
using Smartflow.Elements;
using Smartflow.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Smartflow
{
    public class WorkflowRuleService : WorkflowInfrastructure, IWorkflowPersistent<Rule, Action<string, object>>, IWorkflowQuery<IList<Rule>, string>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Elements.Rule
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Rule entry, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_NODE_RULE_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Rule> Query(String instanceID)
        {
            return base.Connection.Query<Rule>(ResourceManage.SQL_WORKFLOW_NODE_RULE_SELECT,
                new { InstanceID = instanceID })
                .ToList();
        }
    }
}
