using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Fotobel.Minification;

namespace Fotobel.App_Start.Minification
{
    public class BaseBundle : AbstractBundleConfiguration
    {

        public static string StyleBundle => "~/Content/base/style";
        public static string ScriptBundle => "~/bundles/base/script";
        public override void RegisterBundles(BundleCollection bundles)
        {
            // css
            var bb = new StyleBundle(StyleBundle);
            bb.Include("~/Plugins/fullcalendar/dist/fullcalendar.css");
            bb.Include("~/Plugins/owl-carousel/owl.carousel.css");
            bb.Include("~/Plugins/owl-carousel/owl.theme.css");
            bb.Include("~/Plugins/uikit/css/uikit.min.css");
            bb.Include("~/Plugins/crop/croppic.css");
            bb.Include("~/Plugins/x-editable/xeditable.css");
            bb.Include("~/Plugins/angucomplete-alt/angucomplete-alt.css");
            bb.Include("~/Plugins/jquery-timepicker-jt/jquery.timepicker.css");
            bb.Include("~/Plugins/ng-tags-input/ng-tags-input.css");
            bb.Include("~/Plugins/textAngular/dist/textAngular.css");
            bundles.Add(bb);


            // js
            var bundle = new ScriptBundle(ScriptBundle);
            bundle.Include("~/Plugins/jquery/dist/jquery.js");
            bundle.Include("~/Plugins/moment/moment.js");
            bundle.Include("~/Plugins/angular/angular.js");
            bundle.Include("~/Plugins/angular-route/angular-route.js");

            bundle.Include("~/Plugins/moment/locale/ru.js");
            bundle.Include("~/Plugins/fullcalendar/dist/fullcalendar.js");
            bundle.Include("~/Plugins/fullcalendar/dist/gcal.js");
            bundle.Include("~/Plugins/fullcalendar/dist/lang-all.js");

            bundle.Include("~/Plugins/angular-ui-calendar/src/calendar.js");
            bundle.Include("~/Plugins/uikit/js/uikit.js");
            bundle.Include("~/Plugins/uikit/js/core/modal.js");
            bundle.Include("~/Plugins/jquery-timepicker-jt/jquery.timepicker.js");
            bundle.Include("~/Plugins/angular-jquery-timepicker/src/timepickerdirective.js");


            //jquery-timepicker-jt/jquery.timepicker.js
            //bundle.Include("~/Plugins/uikit/js/uikit.js");
            bundle.Include("~/Plugins/angular-animate/angular-animate.min.js");
            bundle.Include("~/Plugins/ui-bootstrap/ui-bootstrap-tpls-1.1.0.min.js");
            bundle.Include("~/Plugins/uikit/js/components/slideshow.min.js");
            bundle.Include("~/Plugins/uikit/js/components/slideshow-fx.min.js");
            bundle.Include("~/Plugins/yandex-map/ya-map-2.2.js");
            //bundle.Include("~/Plugins/ng-img-crope/ng-img-crop.js");
            bundle.Include("~/Plugins/crop/croppic.min.js");
            bundle.Include("~/Plugins/x-editable/xeditable.min.js");
            bundle.Include("~/Plugins/angucomplete-alt/angucomplete-alt.js");
            bundle.Include("~/Scripts/jquery.timeago.js");
            bundle.Include("~/Scripts/jquery.timeago.rus.js");
            bundle.Include("~/Plugins/ng-tags-input/ng-tags-input.js");
            bundle.Include("~/Plugins/angular-crumble/crumble.js");
            bundle.Include("~/Plugins/ng-file-upload/ng-file-upload-shim.min.js");
            bundle.Include("~/Plugins/ng-file-upload/ng-file-upload.min.js");
            bundle.Include("~/Plugins/angular-treeview/angular.treeview.min.js");
            bundle.Include("~/Plugins/angular-ui-tree/dist/angular-ui-tree.js");
            bundle.Include("~/Plugins/ng-file-upload/ng-file-upload.min.js");
            bundle.Include("~/Plugins/codemirror/lib/codemirror.js");
            bundle.Include("~/Plugins/codemirror/addon/mode/simple.js");
            bundle.Include("~/Plugins/codemirror/addon/mode/multiplex.js");
            bundle.Include("~/Plugins/codemirror/mode/htmlmixed/htmlmixed.js");
            bundle.Include("~/Plugins/codemirror/mode/handlebars/handlebars.js");
          

            bundle.Include("~/Plugins/codemirror/mode/javascript/javascript.js");
            bundle.Include("~/Plugins/codemirror/mode/css/css.js");
            bundle.Include("~/Plugins/angular-ui-codemirror/ui-codemirror.min.js");
      


            bundle.Include("~/Plugins/textAngular/dist/textAngular-rangy.min.js");
            bundle.Include("~/Plugins/textAngular/dist/textAngular-sanitize.min.js");
            bundle.Include("~/Plugins/textAngular/dist/textAngular.min.js");


            bundle.Include("~/Plugins/moment/moment.js");

            bundle.Include("~/Plugins/-animate/angular-animate.min.js");

            bundle.Include("~/Plugins/intl-tel-input/build/js/intlTelInput.min.js");

            bundle.Include("~/Plugins/intl-tel-input/lib/libphonenumber/build/utils.js");

            bundle.Include("~/Plugins/angular-moment/angular-moment.min.js");

            bundle.Include("~/Plugins/angular-cookie/angular-cookie.js");

            bundle.Include("~/Plugins/international-phone-number/releases/international-phone-number.min.js");

            bundle.Include("~/Plugins/angular-local-storage/dist/angular-local-storage.min.js");

            bundle.Include("~/Plugins/nprogress/nprogress.js");

            bundle.Include("~/Plugins/angucomplete-alt/angucomplete-alt.js");

            bundle.Include("~/Plugins/ng-tags-input/ng-tags-input.min.js");

            bundle.Include("~/Plugins/angular-crumble/crumble.js");

            bundle.Include("~/Plugins/angularjs-slider/dist/rzslider.min.js");

            bundle.Include("~/Plugins/Chart.js/Chart.js");

            bundle.Include("~/Plugins/angular-chart.js/dist/angular-chart.min.js");

            bundle.Include("~/Plugins/sweetalert2/dist/sweetalert2.min.js");

            bundle.Include("~/Plugins/ng-img-crope/ng-img-crop.js");

            // bundle.Include("~/Plugins/ng-photo-grid-master/js/ngPhotoGrid.js");

            bundle.Include("~/Plugins/jquery-ui/jquery-ui.min.js");

            bundle.Include("~/Plugins/angular-ui-sortable/sortable.js");
            bundle.Include("~/Plugins/angular-soundmanager2/dist/angular-soundmanager2.min.js");
            bundles.Add(bundle);
        }
    }
}
