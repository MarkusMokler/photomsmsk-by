using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Photographer;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PhotographerController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhotographerService _photographerService;

        public PhotographerController(IPhotographerService photographerService)
        {
            _photographerService = photographerService;
        }

        public ActionResult Index(int from = 0, int pageSize = 20)
        {
            var query = _context.Photographers;

            var page = _photographerService.Where(new PageRequest<Photographer> { Page = from / pageSize, Size = pageSize });

            var model = new IndexViewData
                {
                    Page = page
                };

            return View(model);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var photographer = _context.Photographers
                .SingleOrDefault(x => x.Shortcut == id);

            if (photographer == null)
                return HttpNotFound();

            var model = new DetailsViewData
            {
                Item = Mapper.Map<PhotographerViewModel.Details>(photographer)
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