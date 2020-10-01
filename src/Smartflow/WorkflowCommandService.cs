using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Elements;
using Dapper;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowCommandService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Command, Action<string, object>>, IWorkflowQuery<IList<Elements.Command>, string>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return (element.HasElements) ? new Command
            {
                ID = element.Elements("id").FirstOrDefault().Value,
                Text = element.Elements("text").FirstOrDefault().Value
            } : null;
        }

        public void Persistent(Command entry, Action<string, object> callback)
        {
            callback(ResourceManage.SQL_WORKFLOW_NODE_COMMAND_INSERT, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Text,
                entry.InstanceID
            });
        }

        public IList<Command> Query(string instanceID)
        {
            return base.Connection.Query<Command>(ResourceManage.SQL_WORKFLOW_NODE_COMMAND_SELECT, new
            {
                InstanceID = instanceID
            }).ToList();
        }
    }
}
