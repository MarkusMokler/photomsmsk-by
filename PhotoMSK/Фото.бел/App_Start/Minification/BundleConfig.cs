using System;
using System.Linq;
using System.Web.Optimization;

namespace Fotobel.Minification
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

          

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include("~/Content/jquery-ui-1.10.4.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                           "~/Scripts/modernizr-*")
                           );


        }
    }
}
