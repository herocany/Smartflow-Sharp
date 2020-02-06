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
        public readonly BaseBridgeService baseBridgeService = new BaseBridgeService();

        public dynamic Get(string id)
        {
            return baseBridgeService.GetJumpProcess(id);
        }
    }
}