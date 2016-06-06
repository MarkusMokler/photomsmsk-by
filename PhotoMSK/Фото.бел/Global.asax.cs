using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Razor;
using System.Web.Routing;
using System.Web.WebPages;
using Fotobel.App_Start;

namespace Fotobel
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = AutofacConfig.Register();
            AutomapperConfiguration.Configure(container);
            QuartzStart.Run(container);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new BinaryIntellectViewEngine());
        }
    }
}
