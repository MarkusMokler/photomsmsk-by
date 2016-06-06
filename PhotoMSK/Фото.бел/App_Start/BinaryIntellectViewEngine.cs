using System.Web.Mvc;

namespace Fotobel
{
    public class BinaryIntellectViewEngine : RazorViewEngine
    {
        public BinaryIntellectViewEngine()
        {
            string[] locations = {
            "~/Themes/Photorent/{1}/Views/Shared/{0}.cshtml",
            "~/Themes/Photoshop/{1}/Views/Shared/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/{1}/{0}.cshtml"
        };
            ViewLocationFormats = locations;
            PartialViewLocationFormats = locations;
            MasterLocationFormats = locations;
        }
    }
}