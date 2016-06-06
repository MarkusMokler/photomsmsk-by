using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using PhotoMSK.Data;

namespace Fotobel.Api.Version2
{
    public class TemplateController : ApiController
    {
        readonly AppContext _context = new AppContext();

        public HttpResponseMessage Get(string viewName, string themeName = "Default")
        {
            var first = _context.Razorviews.FirstOrDefault(x => x.ViewName == viewName && x.ThemeName == themeName);
            if (first != null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Content = new StringContent(first.Content, System.Text.Encoding.UTF8, "text/plain");
                return resp;
            }
            else
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(null, viewName);
                    ViewContext viewContext = new ViewContext(null, viewResult.View, null, null, sw);
                    viewResult.View.Render(viewContext, sw);

                    var resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(sw.GetStringBuilder().ToString(), System.Text.Encoding.UTF8, "text/plain");
                    return resp;

                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
