using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;

namespace Smartflow.Web.Mvc.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();

        public User Get(string id)
        {
            return userService.Get(id);
        }
    }
}
