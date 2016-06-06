using System.Web.Optimization;

namespace Fotobel.Minification.Styles
{
    public class WhiteLabelAreaStyles : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/WhiteLabel")
                .Include("~/Content/css/WhiteLabel/WhiteLabel.css")
                .Include("~/Content/css/WhiteLabel/WhiteLabel.min.css"));
        }
    }
}