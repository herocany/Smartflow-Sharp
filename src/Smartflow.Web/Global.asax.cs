using Smartflow.Bussiness.Interfaces;
using Smartflow.Bussiness.Models;
using Smartflow.Bussiness.Queries;
using Smartflow.Bussiness.WorkflowService;
using Smartflow.Common;
using Smartflow.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace Smartflow.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;
            config.Filters.Add(new ArgumentCheckAttribute());

            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver();

            dependencyResolver.Register<ISummaryService, SummaryService>();
            dependencyResolver.Register<IBridgeService, BridgeService>();
            dependencyResolver.Register<IQuery<IList<Category>>, CategoryService>();
            dependencyResolver.Register<IPendingService, PendingService>();
            dependencyResolver.Register<IQuery<List<Record>, string>, RecordService>();
            dependencyResolver.Register<IQuery<IList<Constraint>>, ConstraintService>();
            dependencyResolver.Register<AbstractBridgeService, BaseBridgeService>();
            dependencyResolver.Register<IOrganizationService, OrganizationService>();
            dependencyResolver.Register<IActorService, ActorService>();

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            //注册全局的动作 即每跳转一个节点，都会执行动作。
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(PendingAction));
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(RecordAction));

            //抄送动作
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(CarbonCopyAction));

            //注册局部动作 即跳转到特定节点中执行的动作
            WorkflowGlobalServiceProvider.RegisterPartService(new EmptyAction());

            //config.Routes.MapHttpRoute(
            //       name: "DefaultApi",
            //       routeTemplate: "api/{controller}/{id}",
            //       defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                   name: "ActionApi",
                   routeTemplate: "api/{controller}/{action}/{id}",
                   defaults: new { id = RouteParameter.Optional });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Headers.AllKeys.Contains("Origin") && String.Equals(Request.HttpMethod, "OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                Response.Flush();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}