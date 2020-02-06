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
    public class CategoryController : ApiController
    {
        private readonly IQuery<IList<Category>> queryService = new CategoryQueryService();

        public IEnumerable<Category> Get()
        {
            return queryService.Query();
        }

        public Category Get(string id)
        {
            return queryService.Query().FirstOrDefault(cate => cate.NID == id);
        }
    }
}