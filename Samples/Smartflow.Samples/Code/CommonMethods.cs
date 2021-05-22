using Smartflow.BussinessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Smartflow.Samples.Controllers
{
    public class CommonMethods
    {
        public static string Start(string id, string categoryCode, string creator, string comment = null)
        {
            using var client = new HttpClient();
            var arg = new
            {
                CategoryCode = categoryCode,
                Key = id,
                Comment = comment,
                Creator = creator
            };

            HttpContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(arg),System.Text.Encoding.UTF8,"application/json");
            Task<HttpResponseMessage> response =
                client.PostAsync(SystemConstraint.CONST_WORKFLOW_START, content);

            return response.Result.Content.ReadAsStringAsync().Result;
        }
    }
}