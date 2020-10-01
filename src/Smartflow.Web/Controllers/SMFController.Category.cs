using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;

namespace Smartflow.Web.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IQuery<IList<Category>> _categoryService;

        public CategoryController(IQuery<IList<Category>> categoryService)
        {
            _categoryService = categoryService;
        }

        public IEnumerable<Category> Get()
        {
            return _categoryService.Query();
        }

        public Category Get(string id)
        {
            return _categoryService.Query().FirstOrDefault(cate => cate.NID == id);
        }
    }
}