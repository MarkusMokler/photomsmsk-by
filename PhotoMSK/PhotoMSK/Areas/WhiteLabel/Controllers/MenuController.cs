using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Models;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{
    [Authorize]
    [RouteID]
    public class MenuController : WhiteLabelController
    {
        private readonly AppContext _context = new AppContext();
        // GET: WhiteLabel/Menu
        public ActionResult Index()
        {
            var id = RouteID;
            var list = _context.AbstractMenuItem.Where(x=>x.RouteID==RouteID).ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            var menuItemEditModel = new MenuItemEditModel {RouteID = RouteID};
            return View(menuItemEditModel);
        }
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}