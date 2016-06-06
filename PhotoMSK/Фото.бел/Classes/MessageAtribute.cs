using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Fotobel.Classes
{
    public class MessageAttribute : ActionFilterAttribute
    {
        private readonly string _message;

        public MessageAttribute(string message)
        {
            _message = message;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                return;
            actionExecutedContext.Response.Headers.Add("X-ResponseStatus", Base64Encode(_message));
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}