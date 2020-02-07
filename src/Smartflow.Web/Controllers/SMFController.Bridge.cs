using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Commands;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public class BridgeController : ApiController
    {
        private readonly IQuery<Bridge, string> queryService = new BridgeQueryService();
        private readonly IQuery<Bridge, Dictionary<string, string>> service = new BridgeQueryService();

        public Bridge Get(string id)
        {
            return queryService.Query(id);
        }

        public Bridge Get(string id, string categoryId)
        {
            Dictionary<string, string> queryArg = new Dictionary<string, string>
            {
                { "FormID", id },
                { "CategoryID", categoryId }
            };
            return service.Query(queryArg);
        }

        public void Post(Bridge model)
        {
            CommandBus.Dispatch<Bridge>(new CreateBridge(), model);
        }
    }
}