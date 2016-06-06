using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Fotobel.Classes;

namespace Fotobel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // filters.Add(new Error());
        }
    }

    public class Error : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception.Source == "EntityFramework")
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable,
                        new ValidationException(
                            $"На данный момент сайт обновляется до {GetType().Assembly.GetName().Version} версии . Попробуйте через пару минут",
                            MessageCodes.OkAction));

                    return;
                }

            }
            base.OnException(context);
        }
    }

}
