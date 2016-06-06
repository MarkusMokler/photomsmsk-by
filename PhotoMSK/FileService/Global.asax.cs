using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using FileService.App_Start;

namespace FileService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
