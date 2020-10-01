/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: https://www.smartflow-sharp.com
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Smartflow.Elements;
using Smartflow.Internals;
using Dapper;

namespace Smartflow
{
    public class WorkflowService : AbstractWorkflow
    {
        public override string Start(string resourceXml)
        {
            Workflow workflow = XMLServiceFactory.Create(resourceXml);
            var start = workflow.Nodes.Where(n => n.NodeType == WorkflowNodeCategory.Start).FirstOrDefault();

            IList<Action<IDbConnection, IDbTransaction, string>> commands = new List<Action<IDbConnection, IDbTransaction, string>>();
            string callback(IDbConnection connection, IDbTransaction transaction) => InstanceService.CreateInstance(start.ID, resourceXml, (command, entry) => connection.Execute(command, entry, transaction));

            foreach (Node node in workflow.Nodes)
            {
                commands.Add((connection, transaction, id) =>
                {
                    node.InstanceID = id;
                    base.NodeService.Persistent(node, (command, entry) => connection.Execute(command, entry, transaction));
                });
            }

            return DbFactory.Execute(callback, commands);
        }
    }
}
