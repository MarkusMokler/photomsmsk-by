using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

using DDay.iCal;
using DDay.iCal.Serialization;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;
using PhotoMSK.Models.EditViewModel.Routes;
using PhotoMSK.ViewModels.Route;
using Event = PhotoMSK.Data.Models.Event;


namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class PhotostudioController : Controller
    {
        private readonly AppContext _context = new AppContext();

        public ActionResult Index(string shortcut)
        {
            var studio = _context.Photostudios.Single(x => x.Shortcut == shortcut).As<PhotostudioViewModel.Detaills>();
            return View(studio);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Profit(string shortcut)
        {
            var studio = _context.Photostudios.Single(x => x.Shortcut == shortcut);
            return View(studio);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photostudio photostudio)
        {
            if (_context.Routes.Any(x => x.Shortcut == photostudio.Shortcut))
            {
                ModelState.AddModelError("Shortcut", "Такой короткий адрес уже есть");
                return View(photostudio);
            }

            if (ModelState.IsValid)
            {
                string id = User.Identity.GetUserId();
                User currentUser = _context.Users.Find(id);

                photostudio.ID = Guid.NewGuid();
                photostudio.Actuality = DateTime.Now;

                currentUser.UserInformation.Roles.Add(new Role
                {
                    ID = Guid.NewGuid(),
                    AccessStatus = AccessStatus.Owner,
                    Route = photostudio
                });

                currentUser.UserMenuItems.Add(new UserMenuItem
                {
                    ID = Guid.NewGuid(),
                    Action = "Photostudio",
                    Controller = "Index",
                    Index = 1,
                    Shortcut = photostudio.Shortcut,
                    Title = "Бронирование - " + photostudio.Name
                });

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photostudio);
        }

        public ActionResult Create2(Photostudio photostudio)
        {
            if (_context.Routes.Any(x => x.Shortcut == photostudio.Shortcut))
            {
                ModelState.AddModelError("Shortcut", "Такой короткий адрес уже есть");
                return View(photostudio);
            }

            if (ModelState.IsValid)
            {
                string id = User.Identity.GetUserId();
                User currentUser = _context.Users.Find(id);

                photostudio.ID = Guid.NewGuid();
                photostudio.Actuality = DateTime.Now;

                currentUser.UserInformation.Roles.Add(new Role
                {
                    ID = Guid.NewGuid(),
                    AccessStatus = AccessStatus.Owner,
                    Route = photostudio
                });

                currentUser.UserMenuItems.Add(new UserMenuItem
                {
                    ID = Guid.NewGuid(),
                    Action = "Photostudio",
                    Controller = "Index",
                    Index = 1,
                    Shortcut = photostudio.Shortcut,
                    Title = "Бронирование - " + photostudio.Name
                });

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photostudio);
        }

        [Authorize]
        public ActionResult Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photostudio photostudio = _context.Photostudios.SingleOrDefault(x => x.Shortcut == shortcut);
            if (photostudio == null)
            {
                return HttpNotFound();
            }
            return View(photostudio.As<PhotostudioEditModel>());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photostudio photostudio)
        {
            if (ModelState.IsValid)
            {
                var dbstudio = _context.Photostudios.Find(photostudio.ID);

                photostudio.UpdateValues(dbstudio);

                dbstudio.Actuality = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photostudio);
        }

        public ActionResult Delete(string shortcut)
        {
            if (shortcut != "abcd")
                return Redirect("/");
            List<string> shortcuts = new List<string>() { "test", "shortcut1", "qqqqqq", "anything", "dqdqwd", "name123321name", "ttttttttt", "eefwefewf" };
            foreach (var str in shortcuts)
            {
                Photostudio photostudio = _context.Photostudios.SingleOrDefault(x => x.Shortcut == str);

                if (photostudio == null)
                {
                  continue;
                }
                foreach (var hall in photostudio.Halls.ToList())
                {
                    foreach (var photo in hall.Photos.ToList())
                    {
                        _context.Entry(photo).State = EntityState.Deleted;
                    }
                    _context.Entry(hall).State = EntityState.Deleted;

                }
                _context.Entry(photostudio.Raiting).State = EntityState.Deleted;

                foreach (var parts in photostudio.Participants.ToList())
                {
                    _context.Entry(parts).State = EntityState.Deleted;
                }

                foreach (var photo in photostudio.Photos.ToList())
                {
                    _context.Entry(photo).State = EntityState.Deleted;
                }


                _context.Entry(photostudio).State = EntityState.Deleted;
            }

           
            _context.SaveChanges();
            return Redirect("/");
        }


        public ActionResult ICalendar(string shortcut, Guid hallID, string accessToken)
        {

            var iCal = new iCalendar();

            var studio = _context.Photostudios.Single(x => x.Shortcut == shortcut);

            var hall = studio.Halls.Single(x => x.ID == hallID);

            foreach (var @event in hall.Calendar.Events)
            {

                var evt = iCal.Create<DDay.iCal.Event>();

                // Set information about the event
                evt.Start = new iCalDateTime(@event.Start);
                evt.End = new iCalDateTime(@event.End); // This also sets the duration
                evt.Description = @event.Description;
                evt.Location = hall.Photostudio.Adress;
                evt.Summary = "18 hour event summary";
                evt.UID = @event.ID.ToString();
            }

            // Create a serialization context and serializer factory.
            // These will be used to build the serializer for our object.
            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new DDay.iCal.Serialization.iCalendar.SerializerFactory();
            // Get a serializer for our object
            IStringSerializer serializer = factory.Build(iCal.GetType(), ctx) as IStringSerializer;

            string output = serializer.SerializeToString(iCal);
            var contentType = "text/calendar";
            var bytes = Encoding.UTF8.GetBytes(output);

            return File(bytes, contentType, hallID + ".ics");
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