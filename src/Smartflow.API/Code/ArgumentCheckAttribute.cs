/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Smartflow.API.Code
{
    public class ArgumentCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                IList<string> errors = new List<string>();
                foreach (string key in actionContext.ModelState.Keys)
                {
                    if (actionContext.ModelState.TryGetValue(key, out ModelStateEntry modelError))
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
                actionContext.Result = new JsonResult(data);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}