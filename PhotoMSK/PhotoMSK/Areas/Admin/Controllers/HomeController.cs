using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly AppContext _context = new AppContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var id = this.User.Identity.GetUserId();
            return View(_context.Users.Find(id));
        }

        public ActionResult ListUserAccounts()
        {
            return View();
        }

        public ActionResult UserMenu()
        {
            return View();
        }

        public ActionResult LeftAdministrativeMenu()
        {
            var id = this.User.Identity.GetUserId();
            return View(_context.Users.Find(id));
        }

        public ActionResult Chat()
        {
            var userList = _context.Users.Include(x => x.UserInformation).Take(20).ToList();
            return View(userList);
        }

        public ActionResult UserInformation()
        {
            string id = User.Identity.GetUserId();
            User user = _context.Users.Find(id);

            return View(user.UserInformation);
        }

        /// <summary>
        ///  Добавляет значения прайска к ивентам )
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPrice()
        {
            var list = _context.Events.ToList();

            foreach (var @event in list)
            {
                @event.Price = @event.Calendar.GetPrice(@event.Start);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}