using Refit;
using Smartflow.BussinessService.Interfaces;
using Smartflow.BussinessService.Models;
using System;
using System.Threading.Tasks;

namespace Smartflow.BussinessService.Services
{
    public class WorkflowService : IWorkflowService
    {
        public async Task<string> StartAsync(Brigde brigde)
        {
            IWorkflowService service = RestServiceExtensions.For<IWorkflowService>();
            return await service.StartAsync(brigde);
        }
    }
}
