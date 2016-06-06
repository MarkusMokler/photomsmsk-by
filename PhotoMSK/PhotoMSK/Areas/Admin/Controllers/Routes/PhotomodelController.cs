using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models.EditViewModel.Routes;
using PhotoMSK.ViewModels.Realisation;


namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotomodelController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index(string shortcut)
        {
            var studio = _context.Photomodels.Single(x => x.Shortcut == shortcut);
            return View(studio);
        }
        public ActionResult Create()
        {
            return View();
        }

     
        public ActionResult Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photomodel photomodel = _context.Photomodels.SingleOrDefault(x => x.Shortcut == shortcut);

            if (photomodel == null)
            {
                return HttpNotFound();
            }
            return View(photomodel.As<PhotomodelEditModel>());
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