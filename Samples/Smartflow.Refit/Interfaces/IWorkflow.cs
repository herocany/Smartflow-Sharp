using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.Refit.Interfaces
{
    public interface IWorkflow
    {
        [Post("/api/smf/start")]
        Task Start();
    }
}
