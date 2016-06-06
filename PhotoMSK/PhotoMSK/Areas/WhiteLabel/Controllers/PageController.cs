using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Data;
using PhotoMSK.Models;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel;
using PhotoMSK.Models.EditViewModel.Routes;

namespace PhotoMSK.Areas.WhiteLabel.Controllers
{
    [Authorize]
    [RouteID]
    public class PageController : WhiteLabelController
    {
        readonly AppContext _context = new AppContext();

        public ActionResult Index()
        {
            var id = RouteID;
            var list = _context.TextPages.Where(x => x.RouteID == id).ToList();

            return View(list);
        }

        public ActionResult Create()
        {
            var model = new TextPageEditModel() { RouteID = RouteID };

            return View(model);
        }

        public ActionResult Create2()
        {
            var model = new TextPageEditModel() { RouteID = RouteID };

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var page = _context.TextPages.Find(id);

            if (page == null)
                return HttpNotFound();

            if (page.RouteID != RouteID)
                return HttpNotFound();

            var pemodel = Mapper.Map<TextPageEditModel>(page);
            return View("Create",pemodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}