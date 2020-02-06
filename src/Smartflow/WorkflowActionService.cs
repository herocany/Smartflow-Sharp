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
    public class WorkflowActionService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Action, Action<string, object>>, IWorkflowQuery<IList<Elements.Action>, string>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return new Elements.Action
            {
                Name = element.Attribute("name").Value,
                ID = element.Attribute("id").Value
            };
        }

        public void Persistent(Elements.Action entry,Action<string, object> execute)
        {
            execute(ResourceManage.SQL_WORKFLOW_NODE_ACTION_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Name,
                entry.InstanceID
            });
        }

        public IList<Elements.Action> Query(String instanceID)
        {
            return base.Connection.Query<Elements.Action>(ResourceManage.SQL_WORKFLOW_NODE_ACTION_SELECT,
                new { InstanceID = instanceID })
                .ToList();
        }
    }
}

