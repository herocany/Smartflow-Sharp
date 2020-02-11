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
    public class SettingController : ApiController
    {
        public readonly BaseBridgeService baseBridgeService = new BaseBridgeService();

        [HttpGet]
        public IEnumerable<WorkflowGroup> GetGroup()
        {
            return baseBridgeService.GetGroup();
        }

        [HttpGet]
        public IEnumerable<dynamic> GetAction()
        {
            IList<dynamic> types = new List<dynamic>();
            foreach (IWorkflowAction action in WorkflowGlobalServiceProvider.QueryActions())
            {
                Type type = action.GetType();
                types.Add(new
                {
                    id = type.FullName,
                    name = type.Name
                });
            }
            return types;
        }

        [HttpGet]
        public IEnumerable<WorkflowConfiguration> GetDatabaseSourceList()
        {
            return baseBridgeService.GetDatabaseSourceList();
        }

        [HttpGet]
        public dynamic GetActor([FromUri]PageInfo info)
        {
            return CommonMethods
                .Success(baseBridgeService
                .GetActor(info.Page, info.Limit, out int total, info.Actor, info.Key), total);
        }

        [HttpGet]
        public dynamic GetAssignActor([FromUri]PageInfo info)
        {
            IList<WorkflowActor> list = baseBridgeService.GetActor(info.Actor);
            return CommonMethods.Success(list, list.Count);
        }

        public IEnumerable<Constraint> GetConstraint()
        {
            return new ConstraintQueryService().Query();
        }
    }

    public class PageInfo
    {
        public int Page
        {
            get;
            set;
        }

        public int Limit
        {
            get;
            set;
        }

        public string Actor
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
    }
}