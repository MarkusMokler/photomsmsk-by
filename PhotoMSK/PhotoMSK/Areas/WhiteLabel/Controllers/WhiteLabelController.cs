using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{
    public abstract class WhiteLabelController : Controller
    {
        protected Guid RouteID
        {
            get
            {

                var cookie = Request.Cookies["RouteID"];

                Debug.Assert(cookie != null, "cookie != null");
                return Guid.Parse(cookie.Value);
            }
            set
            {

                var cookie = new HttpCookie("RouteID", value.ToString());
                Response.Cookies.Add(cookie);
            }
        }
    }
}