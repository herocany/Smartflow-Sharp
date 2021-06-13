/********************************************************************
 License: https://github.com/chengderen/Smartflow/blob/master/LICENSE 
 Home page: http://www.smartflow-sharp.com
 Github : https://github.com/chengderen/Smartflow-Sharp
 ********************************************************************
 */
using Microsoft.AspNetCore.Mvc.Filters;
using Smartflow.Common.Logging;
using System.Threading.Tasks;

namespace Smartflow.API.Code
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