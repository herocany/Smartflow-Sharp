using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Elements;
using Smartflow.Web.Code;
using Smartflow.Web.Models;

namespace Smartflow.Web.Controllers
{
    public class ActorController : ApiController
    {
        protected IWorkflowNodeService NodeService
        {
            get
            {
                return WorkflowGlobalServiceProvider.Resolve<AbstractWorkflow>().NodeService;
            }
        }

        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public ResultData GetAuditUser(RequestInstanceDto dto)
        {
            Node current = NodeService.FindNodeByID(dto.Destination, dto.ID);
            Dictionary<String, string> queryArg = new Dictionary<string, string>
            {
                { "instanceID", dto.ID },
                { "nodeID", current.NID}
            };
            IList<User> users = _actorService.Query(queryArg);
            return CommonMethods.Response(users, users.Count);
        }
    }
}