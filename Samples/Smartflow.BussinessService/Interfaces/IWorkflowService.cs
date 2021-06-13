using Refit;
using Smartflow.BussinessService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.BussinessService.Interfaces
{
    public interface IWorkflowService
    {
        [Post("/api/smf/start")]
        Task<string> StartAsync(Brigde brigde);
    }
}
