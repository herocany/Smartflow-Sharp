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
        private readonly AbstractBridgeService _abstractBridgeService;
        private readonly IQuery<IList<Constraint>> _constraintService;


        public SettingController(AbstractBridgeService abstractBridgeService, IQuery<IList<Constraint>> constraintService)
        {
            _abstractBridgeService = abstractBridgeService;
            _constraintService = constraintService;
        }

        [HttpGet]
        public IEnumerable<WorkflowGroup> GetGroup()
        {
            return _abstractBridgeService.GetGroup();
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
            return _abstractBridgeService.GetDatabaseSourceList();
        }

        [HttpGet]
        public dynamic GetActor([FromUri]PageInfo info)
        {
            return CommonMethods
                .Success(_abstractBridgeService
                .GetActor(info.Page, info.Limit, out int total, info.Arg, info.Key), total);
        }

        [HttpGet]
        public dynamic GetAssignActor([FromUri]PageInfo info)
        {
            IList<WorkflowActor> list = _abstractBridgeService.GetActor(info.Arg);
            return CommonMethods.Success(list, list.Count);
        }

        public IEnumerable<Constraint> GetConstraint()
        {
            return _constraintService.Query();
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

        public string Arg
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