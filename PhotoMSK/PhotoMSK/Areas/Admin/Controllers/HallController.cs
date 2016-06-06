using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;


namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class HallController : Controller
    {
        private readonly AppContext _context = new AppContext();
       
        public ActionResult PhotostudioHalls(string shortcut)
        {
            if (shortcut == null)
            {
                return HttpNotFound();
            }

            Photostudio studio = _context.Photostudios.SingleOrDefault(x => x.Shortcut == shortcut);
            if (studio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Shortcut = shortcut;
            return View(studio.Halls);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = _context.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        [Authorize]
        public ActionResult Create(string shortcut)
        {
            var hall = new Hall
            {
                PhotostudioID = _context.Photostudios.Single(x => x.Shortcut == shortcut).ID,
                Photostudio = _context.Photostudios.Single(x => x.Shortcut == shortcut)
            };
            return View(hall);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hall hall)
        {
            if (ModelState.IsValid)
            {
                hall.ID = Guid.NewGuid();
                hall.Calendar = new HallCalendar();
                hall.Schedule = new Collection<ScheduleDay>
                {
                    new ScheduleDay
                    {
                        DayOfWeek = 1,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 2,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 3,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 4,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 5,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 6,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 190000
                    },
                    new ScheduleDay
                    {
                        DayOfWeek = 7,
                        TimeStart = new DateTime(2014, 1, 1, 8, 0, 0),
                        TimeEnd = new DateTime(2014, 1, 1, 20, 0, 0),
                        ID = Guid.NewGuid(),
                        Price = 220000
                    },
                };
                _context.Halls.Add(hall);
                _context.SaveChanges();
                return RedirectToAction("Edit", new {id = hall.ID});
            }

            return View(hall);
        }
        
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = _context.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hall hall)
        {
            if (ModelState.IsValid)
            {
                Hall dbhall = _context.Halls.Find(hall.ID);
                hall.UpdateValues(dbhall);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hall hall = _context.Halls.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }
        
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Hall hall = _context.Halls.Find(id);
            _context.Halls.Remove(hall);
            _context.SaveChanges();
            return RedirectToAction("Index");
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