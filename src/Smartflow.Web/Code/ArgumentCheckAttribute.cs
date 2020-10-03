using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Smartflow.Web.Code
{
    public class ArgumentCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                IList<string> errors = new List<string>();
                foreach (string key in actionContext.ModelState.Keys)
                {
                    if (actionContext.ModelState.TryGetValue(key, out ModelState modelError))
                    {
                        foreach (ModelError error in modelError.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }
                ResultData data = new ResultData(999, "参数不合法");
                if (errors.Count > 0)
                {
                    string errorMessage = errors.Count > 0 && !String.IsNullOrEmpty(string.Join("", errors)) ? string.Join("", errors) : data.Message;
                    data.Message = string.Format("{0}", errorMessage);
                }

                actionContext.Response = new HttpResponseMessage()
                {
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data))
                };
            }
            base.OnActionExecuting(actionContext);
        }
    }
}