using System.Runtime.Serialization.Formatters;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Http.WebHost;
using Fotobel.Api;
using Fotobel.Classes.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fotobel
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            config.Filters.Add(new Error());

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings.Formatting = Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.Converters.Add(new AttachmentConverter());
            json.SerializerSettings.Converters.Add(new AttachmentViewModelConverter());
            json.SerializerSettings.Converters.Add(new WidgetConverter());
 
            config.Routes.MapHttpRoute(
                name: "Template",
                routeTemplate: "Scripts/{*id}",
                defaults: new { id = RouteParameter.Optional, version = "Services", controller = "Templates", action = "Index" }
            );
            config.Routes.MapHttpRoute(
                name: "VersionApi",
                routeTemplate: "api/v{version}/{parentController}/{shortcut}/{controller}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
             name: "Servie",
             routeTemplate: "api/Services/{controller}/{action}/{id}",
             defaults: new { version = "Services", id = RouteParameter.Optional }
         );


            config.Routes.MapHttpRoute(
               name: "VersionApi2",
               routeTemplate: "api/v{version}/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "Photostudio",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(Startup.OAuthServerOptions.AuthenticationType));
            config.Services.Replace(typeof(IHttpControllerSelector), new MskHttpControllerSelector((config)));
        }
    }
}
