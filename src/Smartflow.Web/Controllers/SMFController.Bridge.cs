using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Web.Code;
using Smartflow.Web.Models;

namespace Smartflow.Web.Controllers
{
    public class BridgeController : ApiController
    {
        private readonly IBridgeService _bridgeService;

        public BridgeController(IBridgeService bridgeService)
        {
            _bridgeService = bridgeService;
        }

        public BridgeDto Get(string id)
        {
            return EmitCore.Convert<Bridge, BridgeDto>(_bridgeService.Query(id));
        }

        [HttpGet]
        public BridgeDto GetBridge(string id)
        {
            return EmitCore.Convert<Bridge, BridgeDto>(_bridgeService.GetBridge(id));
        }

        [HttpPost]
        public ResultData GetBridgeByMultipleKeys(BridgeRequestDto dto)
        {
            Dictionary<string, string> queryArg = new Dictionary<string, string>
            {
                { "Key", dto.ID },
                { "CategoryID", dto.CategoryID }
            };
            return CommonMethods.Response(data: _bridgeService.Query(queryArg));
        }
    }
}