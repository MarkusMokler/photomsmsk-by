using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.SuperAdmin.Controllers
{
    public class PlasticCardController : Controller
    {
        // GET: SuperAdmin/PlasticCard
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

        public ActionResult Edit()
        {
            return View();
        }
    }
}