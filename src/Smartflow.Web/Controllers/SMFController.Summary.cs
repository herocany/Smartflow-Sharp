using Smartflow.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Smartflow.Web.Code;
using Smartflow.Common;
using Smartflow.Bussiness.Models;
using Smartflow.Web.Models;
using Smartflow.Bussiness.Commands;

namespace ZTT.MES.Web.Controllers
{
    public class SummaryController : ApiController
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpPost]
        public ResultData Paging(Paging info)
        {
            List<Summary> list = _summaryService.Query(info, out int total).ToList();
            return CommonMethods.Response(EmitCore.Convert<List<Summary>, List<SummaryDto>>(list), total);
        }

        [HttpPost]
        public ResultData PagingSupervise(Paging info)
        {
            List<Summary> list = _summaryService.QuerySupervise(info, out int total).ToList();
            return CommonMethods.Response(EmitCore.Convert<List<Summary>, List<SummaryDto>>(list), total);
        }

        [HttpPost]
        public ResultData Query(Dictionary<string, string> queryArg)
        {
            IList<Summary> list = _summaryService.Query(queryArg);

            return CommonMethods.Response(EmitCore.Convert<IList<Summary>, List<SummaryDto>>(list), list.Count);
        }

        public void Delete(SummaryDeleteDto dto)
        {
            var r = EmitCore.Convert<SummaryDeleteDto, Script>(dto);
            CommandBus.Dispatch(new DeleteAllRecord(), r);
        }
    }
}