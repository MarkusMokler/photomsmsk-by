using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Core;
using MarkdownSharp;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;


namespace PhotoMSK.Extensiolns
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Helper class for transforming Markdown.
        /// </summary>
        /// <summary>
        /// An instance of the Markdown class that performs the transformations.
        /// </summary>
        static readonly Markdown MarkdownTransformer = new Markdown();

        /// <summary>
        /// Transforms a string of Markdown into HTML.
        /// </summary>
        /// <param name="helper">HtmlHelper - Not used, but required to make this an extension method.</param>
        /// <param name="text">The Markdown that should be transformed.</param>
        /// <returns>The HTML representation of the supplied Markdown.</returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string text)
        {
            // Transform the supplied text (Markdown) into HTML.
            string html = MarkdownTransformer.Transform(text);

            // Wrap the html in an MvcHtmlString otherwise it'll be HtmlEncoded and displayed to the user as HTML :(
            return new MvcHtmlString(html);
        }

        public static IDisposable Template(this HtmlHelper helper, string id, DisplayType type = DisplayType.View)
        {
            return new TemplateWriter(helper.ViewContext.Writer, id, type);
        }

        public static MvcHtmlString MskAction(this HtmlHelper helper, string text, string controller, string action)
        {
            TagBuilder builder = new TagBuilder("a");

            builder.SetInnerText(text);

            builder.GenerateId(string.Format("{0}-{1}", controller, action));

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            builder.Attributes.Add("href", urlHelper.MskActionUrl(controller, action).ToString());

            return new MvcHtmlString(builder.ToString());
        }


        public static string MakeUrl(this HtmlHelper helper, RouteEntityViewModel.Summary entity, string action)
        {
            var urlbuilder = DependencyResolver.Current.GetService<IUrlBuilder>();

            return urlbuilder.GetRoutePhoController(new RouteObject() { Action = action, RouteShortcut = entity.Shortcut, WhiteLabel = entity.WhiteLabel });

        }

        public static string MakeUrl(this PageMenuItem menuItem)
        {
            var urlbuilder = DependencyResolver.Current.GetService<IUrlBuilder>();

            return urlbuilder.GetRoutePhoController(new RouteObject() { Action = menuItem.Page.Slug, RouteShortcut = menuItem.RouteEntity.Shortcut, WhiteLabel = menuItem.RouteEntity.WhiteLabel });
        }

        public static string Url(this PhototechnicsViewModel.Details phototechnicsViewModel)
        {

            var msk = HttpContext.Current.Request.Url.Host.Contains("otomsk") ||
               HttpContext.Current.Request.Url.Host.Contains("localhost");
            if (msk)
            {
                var builder = new UriBuilder(HttpContext.Current.Request.Url) { Path = string.Format("catalog/{0}/{1}@{2}", phototechnicsViewModel.CategorySlug, phototechnicsViewModel.Shortcut, phototechnicsViewModel.RouteShortcut) };
                return builder.ToString();
            }
            else
            {
                var builder = new UriBuilder(HttpContext.Current.Request.Url.Host) { Path = string.Format("catalog/{0}/{1}", phototechnicsViewModel.CategorySlug, phototechnicsViewModel.Shortcut) };
                return builder.ToString();
            }

        }
        public static string Url(this PhototechnicsViewModel.Summary phototechnicsViewModel)
        {

            var msk = HttpContext.Current.Request.Url.Host.Contains("otomsk") ||
               HttpContext.Current.Request.Url.Host.Contains("localhost");
            if (msk)
            {
                var builder = new UriBuilder(HttpContext.Current.Request.Url) { Path = string.Format("catalog/{0}/{1}@{2}", phototechnicsViewModel.CategorySlug, phototechnicsViewModel.Shortcut, phototechnicsViewModel.RouteShortcut) };
                return builder.ToString();
            }
            else
            {
                var builder = new UriBuilder(HttpContext.Current.Request.Url.Host) { Path = string.Format("catalog/{0}/{1}", phototechnicsViewModel.CategorySlug, phototechnicsViewModel.Shortcut) };
                return builder.ToString();
            }

        }

        public static string GetUploadUrl(this HtmlHelper helper)
        {
            return ConfigurationManager.AppSettings["FileServer"];
        }

        /// <summary>
        /// Get Current url of application
        /// </summary>
        /// <param name="helper"></param>
        /// <returns>Url of application</returns>
        public static string GetApplicationUrl(this HtmlHelper helper)
        {
            if (HttpContext.Current.Request.ApplicationPath != null)
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority.TrimEnd('/') + "/";
        }

        public static string GetServerUrl(this HtmlHelper helper)
        {
            return ConfigurationManager.AppSettings["ServerUrl"];
        }

        public static string Count<T>(this HtmlHelper helper, IEnumerable<T> ienumerabl) where T : class
        {
            if (ienumerabl == null)
                return "0";
            return " " + ienumerabl.Count();
        }

        public static string Square(this HtmlHelper helper, IEnumerable<HallViewModel.Descriptor> halls)
        {
            if (halls == null)
                return "0";
            return String.Format("{0:0}", halls.Sum(x => x.Square));
        }

        public static string MinimumPrice(this HtmlHelper helper, IEnumerable<HallViewModel.Descriptor> halls)
        {
            if (halls == null)
                return "Не здаётся";
            return String.Format("{0:### ### ### ###}", halls.Min(x => x.TodayPrice));
        }

        public static string Variable(this HtmlHelper helper, string name)
        {
            return string.Format("<%={0} %>", name);
        }
    }


    public enum DisplayType
    {
        View = 0, Summary = 1, Details = 2, Edit = 3
    }

    public class TemplateWriter : IDisposable
    {
        private readonly TextWriter _writer;

        public TemplateWriter(TextWriter writer, string id, DisplayType type)
        {
            var viewtype = GetString(type);
            _writer = writer;
            writer.Write("<script type=\"text/template\" id=\"" + id + "-" + viewtype + "-template\" >");
        }

        private static string GetString(DisplayType type)
        {
            switch (type)
            {
                case DisplayType.View:
                    return "view";
                case DisplayType.Details:
                    return "detalis";
                case DisplayType.Edit:
                    return "edit";
                case DisplayType.Summary:
                    return "summary";
                default:
                    return "";
            }
        }

        public void Dispose()
        {
            _writer.Write("</script>");
        }
    }
}