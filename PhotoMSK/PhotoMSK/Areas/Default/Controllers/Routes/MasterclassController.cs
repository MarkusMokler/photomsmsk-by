using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Configuration;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Masterclass;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class MasterclassController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IMasterclassService _masterclassService;

        public MasterclassController(IMasterclassService masterclassService)
        {
            _masterclassService = masterclassService;
        }

        // GET: /Masterclass/
        public ActionResult Index()
        {
            var data = _masterclassService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        public ActionResult Details(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var masterclass = _context.Masterclasses.SingleOrDefault(x => x.Shortcut == shortcut);
            
            if (masterclass == null)
            {
                return HttpNotFound();
            }

            var model = new DetailsViewData
            {
                Item = Mapper.Map<MasterclassViewModel.Details>(masterclass)
            };

            return View(model);
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