using System.Web.Optimization;

namespace Fotobel.Minification.UIKIT
{
    public class UiKit217 : AbstractUikit
    {
        private const string Version = "2-17-0";
        public static string StyleBundle => $"~/Content/uikit-{Version}/style";
        public static string ScriptBundle => $"~/bundles/uikit-{Version}/script";

        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle(StyleBundle).Include(
                "~/Plugins/uikit-" + Version + "/css/uikit.css")
                .Include("~/Plugins/uikit-" + Version + "/css/uikit.almost-flat.css")
                .IncludeDirectory("~/Plugins/uikit-" + Version + "/css/components", "*.css"));

            var bundle = new ScriptBundle(ScriptBundle);
            foreach (var script in UIKitScripts)
            {
                bundle.Include(string.Format(script, Version));
            }

            bundles.Add(bundle);

        }
    }
}