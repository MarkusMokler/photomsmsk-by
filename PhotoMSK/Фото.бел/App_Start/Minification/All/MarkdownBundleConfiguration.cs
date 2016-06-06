using System.Web.Optimization;

namespace Fotobel.Minification.All
{
    public class MarkdownBundleConfiguration : AbstractBundleConfiguration
    {
        public override void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/markdown/style").Include("~/Plugins/codemirror/lib/codemirror.css"));

            bundles.Add(new ScriptBundle("~/bundles/markdown/script").Include(
                "~/Plugins/codemirror/lib/codemirror.js",
                "~/Plugins/codemirror/mode/markdown/markdown.js",
                "~/Plugins/codemirror/addon/mode/overlay.js",
                "~/Plugins/codemirror/mode/xml/xml.js",
                "~/Plugins/codemirror/mode/gfm/gfm.js",
                "~/Plugins/marked/marked.min.js"
                ));

        }
    }
}