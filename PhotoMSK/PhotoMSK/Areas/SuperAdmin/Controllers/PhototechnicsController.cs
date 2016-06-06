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
    public class PhototechnicsController : Controller
    {
        private readonly AppContext db = new AppContext();


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
        public async Task<ActionResult> Create([Bind(Include = "ID,Shortcut,Format,Name")] Phototechnics phototechnics)
        {
            if (ModelState.IsValid)
            {
                phototechnics.ID = Guid.NewGuid();
                db.Routes.Add(phototechnics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phototechnics);
        }


        [Authorize]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phototechnics phototechnics = await db.Phototechnicses.FindAsync(id);
            if (phototechnics == null)
            {
                return HttpNotFound();
            }
            return View(phototechnics);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Shortcut,Format,Name")] Phototechnics phototechnics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phototechnics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phototechnics);
        }


        [Authorize]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phototechnics phototechnics = await db.Phototechnicses.FindAsync(id);
            if (phototechnics == null)
            {
                return HttpNotFound();
            }
            return View(phototechnics);
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Phototechnics phototechnics = await db.Phototechnicses.FindAsync(id);
            db.Routes.Remove(phototechnics);
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