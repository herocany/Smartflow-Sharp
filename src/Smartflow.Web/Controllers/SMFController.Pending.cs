using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public class PendingController : ApiController
    {
        private readonly IQuery<IList<Pending>, string> command = new PendingQueryService();

        public IEnumerable<Pending> Get(string id)
        {
            return command.Query(id);
        }
    }
}