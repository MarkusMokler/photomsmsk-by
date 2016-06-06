using System.Web.Optimization;

namespace Fotobel.Minification.UIKIT
{
    public class UiKitDefault : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/uikit/style").Include(
                "~/Content/uikit/css/uikit.css",
                "~/Content/uikit/css/uikit.gradient.css")
                .Include("~/Content/uikit//css/uikit.almost-flat.css")
                .IncludeDirectory("~/Content/uikit/css/components", "*.css"));

            bundles.Add(new ScriptBundle("~/bundles/uikit/script").Include(
                "~/Scripts/uikit/uikit.js",
                "~/Scripts/uikit/components/autocomplete.js",
                "~/Scripts/uikit/components/cover.js",
                "~/Scripts/uikit/components/datepicker.js",
                "~/Scripts/uikit/components/form-password.js",
                "~/Scripts/uikit/components/form-select.js",
                "~/Scripts/uikit/components/htmleditor.js",
                "~/Scripts/uikit/components/nestable.js",
                "~/Scripts/uikit/components/notify.js",
                "~/Scripts/uikit/components/pagination.js",
                "~/Scripts/uikit/components/search.js",
                "~/Scripts/uikit/components/slideshow.js",
                "~/Scripts/uikit/components/slideshow-fx.js",
                "~/Scripts/uikit/components/sortable.js",
                "~/Scripts/uikit/components/sticky.js",
                "~/Scripts/uikit/components/timepicker.js",
                "~/Scripts/uikit/components/upload.js"
                ));

            // Подрубаем все JS из UIKit

            bundles.Add(new ScriptBundle("~/bundles/uikit/components/script").IncludeDirectory(
                "~/Scripts/uikit/components/",
                "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/uikit/core/script").IncludeDirectory(
                "~/Scripts/uikit/core/",
                "*.js"));
        }
    }
}