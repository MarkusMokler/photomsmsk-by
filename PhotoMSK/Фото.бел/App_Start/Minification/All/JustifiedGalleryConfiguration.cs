using System.Web.Optimization;

namespace Fotobel.Minification.All
{
    public class JustifiedGalleryConfiguration : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JustifiedGallery")
                .Include("~/Scripts/jquery.justifiedGallery.min.js"));
            bundles.Add(new StyleBundle("~/Content/JustifiedGallery")
                .Include("~/Content/justifiedGallery.min.css"));
        }
    }
}