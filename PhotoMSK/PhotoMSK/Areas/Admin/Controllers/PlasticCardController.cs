using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.Admin.Controllers
{
    public class PlasticCardController : Controller
    {
        readonly AppContext _context = new AppContext();

        // GET: Admin/PlasticCard
        public ActionResult Index()
        {
            var list = _context.SaleCards.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var _plastickCard = _context.SaleCards.Find(id);
            return View(_plastickCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}