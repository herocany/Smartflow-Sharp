using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow
{
    public interface IWorkflowAssistantService
    {
        int GetAssistant(string instanceID, string nodeID);
    }
}
