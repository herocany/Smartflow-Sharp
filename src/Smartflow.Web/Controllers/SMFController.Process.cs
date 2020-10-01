using Smartflow.Bussiness.WorkflowService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Smartflow.Web.Controllers
{
    public class ProcessController : ApiController
    {
        private readonly AbstractBridgeService _abstractService;

        public ProcessController(AbstractBridgeService abstractService)
        {
            _abstractService = abstractService;
        }

        public dynamic Get(string id)
        {
            return _abstractService.GetJumpProcess(id);
        }
    }
}