using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Smartflow.Elements;
using Smartflow.Internals;

namespace Smartflow
{
    public class WorkflowCommandService : WorkflowInfrastructure, IWorkflowPersistent<Elements.Command>, IWorkflowQuery<Elements.Command>, IWorkflowParse
    {
        public Element Parse(XElement element)
        {
            return (element.HasElements) ? new Command
            {
                ID = element.Elements("id").FirstOrDefault().Value,
                Text = element.Elements("text").FirstOrDefault().Value
            } : null;
        }

        public void Persistent(Command entry)
        {
            string sql = "INSERT INTO T_Command(NID,ID,RelationshipID,Text,InstanceID) VALUES(@NID,@ID,@RelationshipID,@Text,@InstanceID)";
            Connection.Execute(sql, new
            {
                NID = Guid.NewGuid().ToString(),
                entry.ID,
                entry.RelationshipID,
                entry.Text,
                entry.InstanceID
            });
        }

        public IList<Command> Query(object condition)
        {
            return base.Connection
                .Query<Command>(" SELECT * FROM T_Command WHERE InstanceID=@InstanceID ", condition).ToList();
        }
    }
}
