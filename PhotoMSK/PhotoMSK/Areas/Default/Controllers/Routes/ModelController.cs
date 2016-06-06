using System;
using System.Net;
using System.Web.Mvc;
using PhotoMSK.Areas.Default.ViewData.Model;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class ModelController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhotomodelService _photomodelService;

        public ModelController(IPhotomodelService photomodelService)
        {
            _photomodelService = photomodelService;
        }

        public ActionResult Index()
        {
            var data = _photomodelService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var photomodel = _context.Photomodels.Find(id)
                .As<PhotomodelViewModel.Details>();

            if (photomodel == null)
            {
                return HttpNotFound();
            }

            var model = new DetailsViewData
            {
                Item = photomodel
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