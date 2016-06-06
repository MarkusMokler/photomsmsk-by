using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PhotoMSK.Data;

using PhotoMSK.Models;

namespace PhotoMSK.Areas.Default.Controllers
{
    [System.Web.Mvc.Authorize]
    public class QrController : Controller
    {
        private readonly AppContext _context = new AppContext();
        // GET: Default/QR
        public ActionResult Index(string id, string pin)
        {
            var card = _context.SaleCards.Single(x => x.CardNumber == id);
            if (card.Pin != pin)
                throw new HttpException();
        
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<CallerHub>();
            var uid = User.Identity.GetUserId();
            hubContext.Clients.User(uid).showInfoByNumber(card.UserInformation.Phones.First().Phone.Number.Replace("+", string.Empty), true);
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}