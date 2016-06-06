using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.ViewModels.Interfaces;

namespace Fotobel.Controllers
{
    public class WhiteLabelController : Controller
    {
        AppContext _context = new AppContext();

        // GET: WhiteLabel
        public ActionResult Index(string whitelabel = "")
        {
            var elem = _context.Routes.Single(x => x.Domain.Equals(whitelabel));
            return View(elem);
        }
    }
}