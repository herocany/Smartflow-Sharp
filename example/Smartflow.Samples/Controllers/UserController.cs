using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Smartflow.BussinessService.Models;
using Smartflow.BussinessService.Services;

namespace Smartflow.Samples.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserService userService = new UserService();

        public string Get(string id)
        {
            var user = userService.Get(id);
            return ToBase64String(Newtonsoft.Json.JsonConvert.SerializeObject(user));
        }

        public static string ToBase64String(string s)
        {
            byte[] byteArray = System.Text.UTF8Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(byteArray);
        }
    }
}
