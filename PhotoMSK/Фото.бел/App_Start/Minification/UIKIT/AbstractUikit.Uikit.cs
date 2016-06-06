using System.Collections.Generic;
using System.Web.Optimization;


namespace Fotobel.Minification.UIKIT
{
    public abstract class AbstractUikit : AbstractBundleConfiguration
    {
        public List<string> UIKitScripts = new List<string> {
                "~/Plugins/uikit-{0}/js/uikit.js",
                "~/Plugins/uikit-{0}/js/components/accordion.js",
                "~/Plugins/uikit-{0}/js/components/autocomplete.js",
                "~/Plugins/uikit-{0}/js/components/cover.js",
                "~/Plugins/uikit-{0}/js/components/datepicker.js",
                "~/Plugins/uikit-{0}/js/components/form-password.js",
                "~/Plugins/uikit-{0}/js/components/form-select.js",
                "~/Plugins/uikit-{0}/js/components/grid.js",
                "~/Plugins/uikit-{0}/js/components/htmleditor.js",
                "~/Plugins/uikit-{0}/js/components/lightbox.js",
                "~/Plugins/uikit-{0}/js/components/nestable.js",
                "~/Plugins/uikit-{0}/js/components/notify.js",
                "~/Plugins/uikit-{0}/js/components/pagination.js",
                "~/Plugins/uikit-{0}/js/components/search.js",
                "~/Plugins/uikit-{0}/js/components/slideshow.js",
                "~/Plugins/uikit-{0}/js/components/slideshow-fx.js",
                "~/Plugins/uikit-{0}/js/components/sortable.js",
                "~/Plugins/uikit-{0}/js/components/sticky.js",
                "~/Plugins/uikit-{0}/js/components/timepicker.js",
                "~/Plugins/uikit-{0}/js/components/tooltip.js",
                "~/Plugins/uikit-{0}/js/components/upload.js"
                };
    }
}