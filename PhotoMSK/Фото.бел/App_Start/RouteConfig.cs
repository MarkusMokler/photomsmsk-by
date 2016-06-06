using System.Web.Mvc;
using System.Web.Routing;
using Fotobel.Classes;

namespace Fotobel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection context)
        {
            context.IgnoreRoute("{resource}.axd/{*pathInfo}");
            context.IgnoreRoute("B6B8CCADD74F5B56FE28541C4139C360.txt");

            context.Add("Subdomain", new DomainRoute("{id}.photomsk.by", "", new { controller = "Home", action = "Index", id = "" }));
            context.Add("WhiteLabel", new WhiteLabelRoute("{whitelabel}", "{*id}", new { controller = "WhiteLabel", action = "Index", id = "", whiteLabel = "" }));
            context.MapRoute("IFrame", "Calendar/{shortcut}", new { controller = "Default", action = "CalendarFrame", shortcut = UrlParameter.Optional });
            context.MapRoute("Default", "{*id}", new { controller = "Default", action = "Index", id = UrlParameter.Optional });
        }

    }
}
