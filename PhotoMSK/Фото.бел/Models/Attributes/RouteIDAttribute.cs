using System.Web.Mvc;
using System.Web.Routing;

namespace Fotobel.Models.Attributes
{
    public class RouteIDAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var fooCookie = actionContext.HttpContext.Request.Cookies["RouteID"];
            if (fooCookie == null)
                actionContext.Result = new RedirectToRouteResult(
              new RouteValueDictionary 
                { 
                    { "controller", "Select" }, 
                    { "action", "Index" } ,
                    {"area","whitelabel"}
                });
        }
    }
}