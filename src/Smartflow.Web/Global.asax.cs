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
            
            //注册全局的动作 即每跳转一个节点，都会执行动作。
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(PendingAction));
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(RecordAction));
            WorkflowGlobalServiceProvider.RegisterGlobalService(typeof(FormAction));

            //注册局部动作即跳转到特定节点中执行的动作
            WorkflowGlobalServiceProvider.RegisterPartService(new EmptyAction());



            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver();


            dependencyResolver.Register<IBridgeService, BridgeService>();
            dependencyResolver.Register<IQuery<IList<Category>>, CategoryService>();
            dependencyResolver.Register<IPendingService, PendingService>();
            dependencyResolver.Register<IQuery<IList<Record>, string>, RecordService>();
            dependencyResolver.Register<IQuery<IList<Constraint>>, ConstraintService>();
            dependencyResolver.Register<AbstractBridgeService, BaseBridgeService>();
            dependencyResolver.Register<IActorService, ActorService>();

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                   name: "ActionApi",
                   routeTemplate: "api/{controller}/{action}/{id}",
                   defaults: new { id = RouteParameter.Optional });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

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