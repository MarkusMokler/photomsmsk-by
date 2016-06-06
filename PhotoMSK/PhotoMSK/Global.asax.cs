using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using PhotoMSK.App_Start;
using PhotoMSK.Classes;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;

namespace PhotoMSK
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = AutofacConfig.Register();
            AutomapperConfiguration.Configure(container);
            QuartzStart.Run();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorThemeViewEngine());
        }


        protected void Application_BeginRequest()
        {
            if (Request.Url.Host.Contains("fotomsk") || Request.IsLocal || Request.Url.Host.Contains("ngweb"))
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            if (Request.Url.Host.Contains("fotomsk") || Request.IsLocal || Request.Url.Host.Contains("ngweb"))
            {
                MiniProfiler.Stop();
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api/");
        }
    }
}
