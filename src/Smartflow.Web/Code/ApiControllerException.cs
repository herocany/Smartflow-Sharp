using Microsoft.AspNetCore.Mvc.Filters;
using Smartflow.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Smartflow.Web.Code
{
    public class ApiControllerException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            LogProxy.Instance.Error(context.Exception);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            LogProxy.Instance.Error(context.Exception);
            return base.OnExceptionAsync(context);
        }
    }
}