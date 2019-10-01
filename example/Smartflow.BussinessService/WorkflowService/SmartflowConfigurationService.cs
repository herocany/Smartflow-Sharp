using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Smartflow.BussinessService.WorkflowService
{
    public class SmartflowConfigurationService : ISmartflowConfigurationService
    {
        public SmartflowConfiguration GetConfiguration()
        {
            ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings["smartflowConnection"];
            return new SmartflowConfiguration
            {
                ConnectionString = settings.ConnectionString,
                ProviderName = settings.ProviderName
            };
        }
    }
}
