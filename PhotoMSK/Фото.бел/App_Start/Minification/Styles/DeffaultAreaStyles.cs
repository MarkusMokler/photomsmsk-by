using System.Web.Optimization;

namespace Fotobel.Minification.Styles
{
    public class DeffaultAreaStyles : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/Default")
                .Include("~/Content/css/Default/Default.css")
                .Include("~/Content/css/Default/Default.min.css"));
        }
    }
}