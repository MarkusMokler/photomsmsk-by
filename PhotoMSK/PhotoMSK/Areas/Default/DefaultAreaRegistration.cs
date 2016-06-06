using System.Web.Mvc;

namespace PhotoMSK.Areas.Default
{
    public class DefaultAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Default"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.Ignore("Token");
            context.MapRoute(
                "Controller",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new
                {
                    controller =
                        @"^(ads|account|calendar|hall|home|masterclass|model|photographer|photorent|photoshop|photostudio|PhototechnicsViewModel|place|page|register|adminBooking|developer|help|statistics|battle|blog|landing|terms)$"
                }
                );

            //context.Routes.Add(new SubdomainRoute());

            MapCatalog(context);

            context.MapRoute("Qr", "qr/{id}/{pin}", new { controller = "QR", action = "Index", id = "", pin = "" });
            context.MapRoute("Wall", "Wall/{id}", new { controller = "Wall", action = "Index", id = "" });

            context.Routes.Add("Subdomain", new DomainRoute("{id}.photomsk.by", "", new { controller = "Home", action = "Index", id = "" }));
            context.Routes.Add("WhiteLabel", new WhiteLabelRoute("{whitelabel}", "", new { controller = "WhiteLabel", action = "Index", id = "", whiteLabel = "" }));

            context.MapRoute("Shorts", "{action}@{id}", new { controller = "Home", action = "Index", id = "", area = AreaName });

            context.MapRoute("Default", "{id}", new { controller = "Home", action = "Index", id = "", area = AreaName });
        }

        private void MapCatalog(AreaRegistrationContext context)
        {
            context.MapRoute("RouteNews", "news@{shortcut}", new { controller = "WhiteLabel", action = "News", id = "", shortcut = "", area = AreaName });
            context.MapRoute("RouteCatalog", "catalog@{shortcut}", new { controller = "Catalog", action = "Index", shortcut = UrlParameter.Optional, area = AreaName });
            context.MapRoute("RouteCatalogCategory", "catalog/{category}@{shortcut}", new { controller = "Catalog", action = "Category", category = "", shortcut = UrlParameter.Optional, area = AreaName });
            context.MapRoute("RouteCatalogCategorySlug", "catalog/{category}/{slug}@{shortcut}", new { controller = "Catalog", action = "Details", shortcut = UrlParameter.Optional, area = AreaName });
            context.MapRoute("RouteCart", "cart@{shortcut}", new { controller = "Catalog", action = "Cart", shortcut = UrlParameter.Optional, area = AreaName });
            context.MapRoute("RoutePage", "{id}@{shortcut}", new { controller = "WhiteLabel", action = "Page", id = "", shortcut = "", area = AreaName });



            context.Routes.Add("WhiteLabelNews", new WhiteLabelRoute("{whitelabel}", "news", new { controller = "Home", action = "Newswl", whiteLabel = "" }));
            context.Routes.Add("WhiteLabelcatalog", new WhiteLabelRoute("{whitelabel}", "catalog", new { controller = "Catalog", action = "Indexwl", id = "", whiteLabel = "" }));
            context.Routes.Add("WhiteLabelcatalogCategory", new WhiteLabelRoute("{whitelabel}", "catalog/{category}", new { controller = "Catalog", action = "Categorywl", id = "", whiteLabel = "" }));
            context.Routes.Add("WhiteLabelcatalogCategorySlug", new WhiteLabelRoute("{whitelabel}", "catalog/{category}/{slug}", new { controller = "Catalog", action = "Detailswl", id = "", whiteLabel = "" }));
            context.Routes.Add("WhiteLabelcart", new WhiteLabelRoute("{whitelabel}", "cart", new { controller = "Catalog", action = "Cart", id = "", whiteLabel = "" }));
            context.Routes.Add("WhiteLabelPage", new WhiteLabelRoute("{whitelabel}", "{id}", new { controller = "WhiteLabel", action = "Pagewl", id = "", whiteLabel = "" }));

        }
    }
}