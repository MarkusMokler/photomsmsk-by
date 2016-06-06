using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class DialogController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index()
        {
            var id = this.User.Identity.GetUserId();
            var elements = _context.Messages.Where(
                 x => x.Adresses.Select(y => y.FromID).Contains(id) || x.Adresses.Select(y => y.ToID).Contains(id))
                 .DistinctBy(x => x.Adresses.First().FromID)
                 .OrderBy(x => x.Time)
                 .Take(10)
                 .ToList();
            return View(elements);
        }

        public ActionResult Detalis(string id)
        {
            var userId = this.User.Identity.GetUserId();
            var list = _context.Adresses.Where(x => (x.ToID == userId && x.FromID == id) || (x.ToID == id && x.FromID == userId))
                .Select(x => x.Message)
                .OrderByDescending(x => x.Time)
                .Take(20).ToList();
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}