using System;
using System.Text;
using System.Web;
using PhotoMSK.ViewModels.Interfaces;

namespace Fotobel
{
    /// <summary>
    /// URL Builder for routes
    /// </summary>
    public class UrlBuilder : IUrlBuilder
    {

        public bool MSK => HttpContext.Current.Request.Url.Host.Contains("otomsk") || HttpContext.Current.Request.Url.Host.Contains("localhost") || HttpContext.Current.Request.Url.Host.Contains("ngweb");

        /// <summary>
        /// Build full url path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="whiteLabel"></param>
        /// <returns></returns>
        public string MakePath(string path,bool whiteLabel=false)
        {
            var request = HttpContext.Current.Request;

            if (MSK||whiteLabel)
            {
                var baseUrl = request.Url.Scheme + "://" + request.Url.Authority;

                var builder = new UriBuilder(baseUrl) { Path = string.IsNullOrEmpty(request.ApplicationPath) ? "" : request.ApplicationPath.TrimEnd('/') + "/" + path.TrimStart('/') };
                return builder.ToString();
            }
            else
            {
                var builder = new UriBuilder("http://photomsk.by") { Path = path };
                return builder.ToString();
            }

        }

        /// <summary>
        /// Build coorect url for routeRequest
        /// </summary>
        /// <param name="shortcut">Shortcut of route</param>
        /// <returns></returns>
        public string GetShortcutUrl(string shortcut)
        {
            return MakePath(shortcut);
        }

        /// <summary>
        /// Generating Url 
        /// </summary>
        /// <param name="oo"> Routes object</param>
        /// <returns>Url</returns>
        public string GetRouteForObject(RouteObject oo)
        {
            return MakePath(MSK ? $"catalog/{oo.CategorySlug}/{oo.Shortcut}@{oo.RouteShortcut}"
                : $"catalog/{oo.CategorySlug}/{oo.Shortcut}",oo.WhiteLabel);
        }

        public string GetRoutePhoController(RouteObject oo)
        {
            StringBuilder builder = new StringBuilder();
            if (oo.Controller != null)
                builder.Append("/" + oo.Controller);
            if (oo.Action != null)
                builder.Append("/" + oo.Action);
            if (oo.Id != null)
                builder.Append("/" + oo.Id);

            return MakePath(MSK ? builder.Append("@" + oo.RouteShortcut).ToString() : builder.ToString(), oo.WhiteLabel);

        }
    }
}
