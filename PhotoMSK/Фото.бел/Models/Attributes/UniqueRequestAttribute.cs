using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace Fotobel.Models.Attributes
{
    public class UniqueRequestAttribute : ActionFilterAttribute
    {
        private static readonly HashSet<Guid> Requests = new HashSet<Guid>();
        Guid ID { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            lock (Requests)
            {
                var result = new System.IO.StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();

                var oo = JObject.Parse(result);
                JToken token;
                ID = Guid.Empty;

                if (oo.TryGetValue("requestID", out token))
                    ID = (Guid)token;

                if (Requests.Contains(ID))
                    throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.Conflict, "Duplicate Request"));
                Requests.Add(ID);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            lock (Requests)
            {
                if (Requests.Contains(ID))
                    Requests.Remove(ID);
            }
        }

        public T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return default(T);
            return (T)HttpContext.Current.Session[key];
        }
    }
}