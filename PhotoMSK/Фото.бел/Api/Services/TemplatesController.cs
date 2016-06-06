using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;


namespace Fotobel.Api.Services
{
    public class TemplatesController : ApiController
    {
        readonly AppContext _context = new AppContext();
        // GET: Templates
        [HttpGet]
        public async Task<HttpResponseMessage> Index(string id)
        {

            string theme = null;
            string path = HttpContext.Current.Server.MapPath("~/Scripts/");
            var response = new HttpResponseMessage();

            var route = _context.Routes.SingleOrDefault(x => x.Domain == Request.Headers.Host);
            if (route != null && (route.Theme != null || route.Theme != string.Empty))
                theme = route.Theme;
            else
                theme = "Default";

            if (theme != "Default")
            {
                var view = _context.Razorviews.SingleOrDefault(
                    x =>
                        x.ViewName == id &&
                        x.ThemeName == theme);

                if (view != null)
                {
                    response.Content = new StringContent(view.Content, Encoding.UTF8, "text/html");
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }

            }

            string line;
            using (var sr = new StreamReader(path + id.Replace("/", @"\") + ".html"))
            {
                line = await sr.ReadToEndAsync();
            }

            if (theme != "Default")
            {
                _context.Razorviews.Add(new HtmlTemplate()
                {
                    ID = Guid.NewGuid(),
                    Content = line,
                    ThemeName = theme,
                    Route = route,
                    ThemeType = "private",
                    ViewName = id
                });
                _context.SaveChanges();
            }
            response.Content = new StringContent(line, Encoding.UTF8, "text/html");
            response.Headers.Add("X-Generated", "foto.bel solutionis");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}