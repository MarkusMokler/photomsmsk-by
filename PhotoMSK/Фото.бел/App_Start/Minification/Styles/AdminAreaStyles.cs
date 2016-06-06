using System.Web.Optimization;

namespace Fotobel.Minification.Styles
{
    public class AdminAreaStyles : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Admin/style")
                //.Include("~/Content/css/Admin/Admin.css")
                .Include("~/Content/css/Admin/icomoon/icomoon.css")
                .Include("~/Content/css/Admin/Admin.css"));
        }
    }
}