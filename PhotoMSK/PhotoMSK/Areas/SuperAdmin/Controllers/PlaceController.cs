using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Models;

namespace PhotoMSK.Areas.SuperAdmin.Controllers
{
    public class PlaceController : Controller
    {
        private readonly AppContext db = new AppContext();

        // GET: /PhototechnicsModel/
        public async Task<ActionResult> Index()
        {
            return View(await db.Places.ToListAsync());
        }

        // GET: /PhototechnicsModel/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = await db.Places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Place place)
        {
            if (ModelState.IsValid)
            {
                place.ID = Guid.NewGuid();
                db.Routes.Add(place);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(place);
        }


        [Authorize]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = await db.Places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Shortcut,Format,Name")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(place);
        }


        [Authorize]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = await db.Places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Place place = await db.Places.FindAsync(id);
            db.Routes.Remove(place);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}