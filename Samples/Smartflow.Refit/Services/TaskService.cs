using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartflow.Refit.Interfaces
{
    public class TaskService: ITaskService
    {
        public async Task<IList<TaskInfo>> GetExpireTasksAsync()
        {
            ITaskService service = RestServiceExtensions.For<ITaskService>();
            return await service.GetExpireTasksAsync();
        }
    }
}
