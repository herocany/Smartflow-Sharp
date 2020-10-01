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
using Smartflow.Components;
using Smartflow.Web.Code;

namespace Smartflow.Web.Controllers
{
    public class SettingController : ApiController
    {
        private readonly AbstractBridgeService _abstractService;
        private readonly IQuery<IList<Constraint>> _constraintService;
        private readonly IOrganizationService _organizationService;

        public SettingController(AbstractBridgeService abstractService, IQuery<IList<Constraint>> constraintService, IOrganizationService organizationService)
        {
            _abstractService = abstractService;
            _constraintService = constraintService;
            _organizationService = organizationService;
        }

        [HttpGet]
        public IEnumerable<WorkflowGroup> GetGroup()
        {
            return _abstractService.GetGroup();
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
            return _abstractService.GetDatabaseSourceList();
        }

        [HttpPost]
        public dynamic GetActor([FromBody]Paging info)
        {
            return CommonMethods.Response(_abstractService
                .GetActor(info.Page, info.Limit, out int total, info.Get()), total);
        }

        [HttpPost]
        public dynamic GetAssignActor([FromBody]Paging info)
        {
            IList<WorkflowActor> list = _abstractService.GetActor(info.Get());
            return CommonMethods.Response(list, list.Count);
        }

        [HttpPost]
        public dynamic Getcarbon([FromBody]Paging info)
        {
            return CommonMethods
                .Response(_abstractService
                .GetCarbon(info.Page, info.Limit, out int total, info.Get()), total);
        }

        [HttpPost]
        public dynamic GetAssignCarbon([FromBody]Paging info)
        {
            IList<WorkflowCarbon> list = _abstractService.GetCarbon(info.Get());
            return CommonMethods.Response(list, list.Count);
        }

        public IEnumerable<Constraint> GetConstraint()
        {
            return _constraintService.Query();
        }

        public IEnumerable<Organization> GetOrgs()
        {
            IList<Organization> orgs = new List<Organization>();
            _organizationService.Load("0", orgs);
            return orgs;
        }

        public IEnumerable<KeyValuePair<string,string>> GetDiscusses()
        {
            IList<KeyValuePair<string, string>> workflowDiscusses = new List<KeyValuePair<string, string>>();
            workflowDiscusses.Add(new KeyValuePair<string, string>(typeof(DemocraticDecision).FullName, "民主决策"));
            workflowDiscusses.Add(new KeyValuePair<string, string>(typeof(FirstDecision).FullName,"第一决策")); ;
            workflowDiscusses.Add(new KeyValuePair<string, string>(typeof(LastDecision).FullName, "最后决策"));
            return workflowDiscusses;
        }

        public IEnumerable<KeyValuePair<string, string>> GetBacks()
        {
            IList<KeyValuePair<string, string>> workflowDiscusses = new List<KeyValuePair<string, string>>();
            workflowDiscusses.Add(new KeyValuePair<string, string>(typeof(FirstDecision).FullName, "第一决策")); ;
            return workflowDiscusses;
        }
    }
}