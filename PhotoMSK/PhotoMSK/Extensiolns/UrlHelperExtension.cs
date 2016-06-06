using System;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Extensiolns
{
    public static class UrlHelperExtension
    {
        public static MvcHtmlString MskActionUrl(this UrlHelper helper, string controller, string action)
        {
            var Request = HttpContext.Current.Request;
            var str = helper.Action(action, controller);

            var msk = HttpContext.Current.Request.Url.Host.Contains("otomsk") || HttpContext.Current.Request.Url.Host.Contains("localhost") || HttpContext.Current.Request.Url.Host.Contains("ngweb");

            if (msk)
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                UriBuilder builder = new UriBuilder(baseUrl) { Path = str };
               
                return new MvcHtmlString(builder.ToString());
            }
            else
            {
                UriBuilder builder = new UriBuilder("http://photomsk.by") { Path = str };
                return new MvcHtmlString(builder.ToString());
            }
        }
    }
}