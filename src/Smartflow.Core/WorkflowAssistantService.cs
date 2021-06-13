/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Smartflow.Common;
using Smartflow.Core.Internals;

namespace Smartflow.Core
{
    public class WorkflowAssistantService : IWorkflowAssistantService
    {
        public int GetAssistant(string instanceID, string nodeID)
        {
            using ISession session = DbFactory.OpenSession();
            WorkflowAssistant workflowAssistant = 
                 session.Query<WorkflowAssistant>()
                        .Where(e => e.InstanceID == instanceID&&e.NodeID==nodeID)
                        .FirstOrDefault();
            
            return workflowAssistant.Total;
        }
    }
}
