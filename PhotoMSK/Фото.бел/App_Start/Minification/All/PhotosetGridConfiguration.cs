using System.Web.Optimization;

namespace Fotobel.Minification.All
{
    public class PhotosetGridConfiguration : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/photoset-grid").Include("~/Scripts/jquery.photoset-grid.js")
                .Include("~/Scripts/colorbox/jquery.colorbox.js"));
            bundles.Add(new StyleBundle("~/Content/colorbox").Include("~/Content/colorbox.css"));
        }
    }
}