/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Smartflow.Core.Elements;
using Smartflow.Core.Internals;
using Smartflow.Common;
using NHibernate;

namespace Smartflow.Core
{
    public class WorkflowService : AbstractWorkflow
    {
        public override string Start(string resourceXml)
        {
            Workflow workflow = XMLServiceFactory.Create(resourceXml);
            var start = workflow.Nodes.Where(n => n.NodeType == WorkflowNodeCategory.Start).FirstOrDefault();
            IList<Action<ISession,string>> commands = new List<Action<ISession, string>>();
            string callback(ISession session) => InstanceService.CreateInstance(start.ID, resourceXml, (entry) => session.Persist(entry));

            foreach (Node node in workflow.Nodes)
            {
                commands.Add((session, id) =>
                {
                    node.InstanceID = id;
                    base.NodeService.Persistent(node, (entry) => session.Persist(entry));
                });
            }
            
            return DbFactory.Execute(DbFactory.OpenSession(), callback, commands);
        }
    }
}
