using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Ads;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Advertising;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;
using PhotoMSK.ViewModels.Advertising;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class AdsController : Controller
    {
        private readonly AppContext _db = new AppContext();

        public AdsController() { }

        public AdsController(AppContext context)
        {
            _db = context;
        }

        // GET: /Adds/
        public async Task<ActionResult> Index()
        {
            var ads = await _db.Ads.ToListAsync();

            var model = new IndexViewData
            {
                Page = { Elements = ads.As<IList<AdViewModel.Details>>()}
            };

            return View(model);
        }

        // GET: /Adds/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ad = await _db.Ads.FindAsync(id);

            if (ad == null)
            {
                return HttpNotFound();
            }

            var model = new DetailsViewData
            {
                Item = Mapper.Map<AdViewModel.Details>(ad)
            };

            return View(model);
        }

        // GET: /Adds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Adds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ID,Title,Subtitle,Image,Description,Views,Visits,Link")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                ad.ID = Guid.NewGuid();
                _db.Ads.Add(ad);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        // GET: /Adds/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = await _db.Ads.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: /Adds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "ID,Title,Subtitle,Image,Description,Views,Visits,Link")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(ad).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        // GET: /Adds/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = await _db.Ads.FindAsync(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: /Adds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Ad ad = await _db.Ads.FindAsync(id);
            _db.Ads.Remove(ad);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult DisplayAddPartial()
        {
            //Ad add = db.Ads.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            //if (add == null)
            //{
            //    return new EmptyResult();
            //}
            //add.Views++;
            //db.SaveChanges();

            return View();
        }

        public ActionResult Go(Guid id)
        {
            Ad add = _db.Ads.Find(id);
            add.Visits++;
            _db.SaveChanges();
            return Redirect(add.Link);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}