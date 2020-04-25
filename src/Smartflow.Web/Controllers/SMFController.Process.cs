using Smartflow.Bussiness.Interfaces;
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
        private readonly AbstractBridgeService _abstractBridgeService;
        public ProcessController(AbstractBridgeService abstractBridgeService)
        {
            _abstractBridgeService = abstractBridgeService;
        }


        public dynamic Get(string id)
        {
            return _abstractBridgeService.GetJumpProcess(id);
        }
    }
}