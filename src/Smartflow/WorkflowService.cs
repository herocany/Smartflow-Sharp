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
    public class WorkflowService : WorkflowInfrastructure, IWorkflow
    {
        protected WorkflowNodeService NodeService
        {
            get
            {
                return new WorkflowNodeService();
            }
        }

        public string Start(string resourceXml)
        {
            Workflow workflow = XMLServiceFactory.Create(resourceXml);
            var start = workflow.Nodes.Where(n => n.NodeType == WorkflowNodeCategory.Start).FirstOrDefault();
            string instaceID = CreateWorkflowInstance(start.ID, resourceXml);
            foreach (Node node in workflow.Nodes)
            {
                node.InstanceID = instaceID;
                NodeService.Persistent(node);
            }
            return instaceID;
        }

        protected string CreateWorkflowInstance(string NID, string resource)
        {
            return WorkflowInstance.CreateWorkflowInstance(NID, resource);
        }
    }
}
