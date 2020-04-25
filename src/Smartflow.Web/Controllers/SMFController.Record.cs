using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public  class RecordController:ApiController
    {
        private readonly IQuery<IList<Record>, string> _recordService;
        public RecordController(IQuery<IList<Record>, string> recordService)
        {
            _recordService = recordService;
        }

        public IEnumerable<Record> Get(string id)
        {
            return _recordService.Query(id);
        }
    }
}