using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Fotobel.Minification;

namespace Fotobel
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            var allConfig = typeof(BundleConfig)
              .Assembly.GetTypes()
              .Where(x => typeof(AbstractBundleConfiguration).IsAssignableFrom(x) && x.IsAbstract == false);

            foreach (var elem in allConfig.Select(config => Activator.CreateInstance(config) as AbstractBundleConfiguration))
            {
                elem.RegisterBundles(bundles);
            }


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.timeago.js",
                "~/Scripts/jquery.rest.js",
                "~/Scripts/locales/jquery.timeago.ru.js",
                "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fullcalendar")
    .Include("~/Content/fullcalendar.css"));

            bundles.Add(new StyleBundle("~/Content/plastic-card")
                .Include("~/Content/plastic-card.css"));

            bundles.Add(new StyleBundle("~/Content/tag-it")
                .Include("~/Content/css/jquery.tagit.css", "~/Content/css/tagit.ui-zendesk.css"));

            bundles.Add(new ScriptBundle("~/bundles/tag-it").Include("~/Content/js/tag-it.js"));

            bundles.Add(new ScriptBundle("~/Scripts/resize").Include("~/Scripts/jquery.autosize.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ionrangeSlider").Include("~/Scripts/ion.rangeSlider.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Chart").Include("~/Scripts/Chart.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/intlTelInput").Include("~/Scripts/intlTelInput.js"));
            bundles.Add(new ScriptBundle("~/Scripts/plastic-card").Include("~/Scripts/plastic-card.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar")
                .Include("~/Scripts/moment.min.js")
                .Include("~/Scripts/fullcalendar.js")
                .Include("~/Scripts/fullcalendar-extension.js")
                );

            bundles.Add(new StyleBundle("~/Content/site-style")
                .Include("~/Content/siteless.css", "~/Content/intlTelInput.css", "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/WhiteLabelLess")
                .Include("~/Content/WhiteLabelLess.css"));



            var application = new ScriptBundle("~/App");
            application.IncludeDirectory("~/Scripts/default/", "*.js", true);
            application.IncludeDirectory("~/Scripts/admin/", "*.js", true);
            application.Transforms.Clear();
            bundles.Add(application);

        }
    }
}
