using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public class BridgeController : ApiController
    {
        private readonly IBridgeService _bridgeService;
        public BridgeController(IBridgeService bridgeService)
        {
            _bridgeService = bridgeService;
        }


        public Bridge Get(string id)
        {
            return _bridgeService.Query(id);
        }

        public Bridge Get(string id, string categoryId)
        {
            Dictionary<string, string> queryArg = new Dictionary<string, string>
            {
                { "Key", id },
                { "CategoryID", categoryId }
            };
            return _bridgeService.Query(queryArg);
        }

        public void Post(Bridge model)
        {
            if (_bridgeService.Query(model.InstanceID) == null)
            {
                CommandBus.Dispatch<Bridge>(new CreateBridge(), model);
            }

            FormService.Execute(model.CategoryID, model.InstanceID);
        }
    }
}