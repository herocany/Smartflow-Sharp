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

namespace Smartflow
{
    public class WorkflowService: AbstractWorkflow
    {
        public override string Start(string resourceXml)
        {

            IDbConnection connection = DbFactory.CreateWorkflowConnection();
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                Workflow workflow = XMLServiceFactory.Create(resourceXml);
                var start = workflow.Nodes.Where(n => n.NodeType == WorkflowNodeCategory.Start).FirstOrDefault();
                string instaceID = InstanceService.CreateInstance(start.ID, resourceXml, workflow.Mode, (command, entry) => connection.Execute(command, entry, transaction));
                foreach (Node node in workflow.Nodes)
                {
                    node.InstanceID = instaceID;
                    base.NodeService.Persistent(node, (command, entry) => connection.Execute(command, entry, transaction));
                }
                transaction.Commit();
                return instaceID;
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return exception.ToString();
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
