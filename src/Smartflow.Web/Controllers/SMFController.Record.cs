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
using Smartflow.Web.Code;
using Smartflow.Web.Models;

namespace Smartflow.Web.Controllers
{
    public  class RecordController:ApiController
    {
        private readonly IQuery<List<Record>, string> _recordService;
        public RecordController(IQuery<List<Record>, string> recordService)
        {
            _recordService = recordService;
        }
     
        public IEnumerable<RecordDto> Get(string id)
        {
            return EmitCore.Convert<List<Record>, List<RecordDto>>(_recordService.Query(id));
        }
    }
}