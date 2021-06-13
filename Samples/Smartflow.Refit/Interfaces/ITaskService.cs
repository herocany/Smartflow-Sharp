using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.Refit.Interfaces
{
    public interface ITaskService
    {
        [Get("/dm/api/task/expire/list")]
        Task GetExpireTasksAsync();
    }
}
