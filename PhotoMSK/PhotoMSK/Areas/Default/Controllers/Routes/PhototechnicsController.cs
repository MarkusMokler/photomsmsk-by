using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PhotoMSK.Areas.Default.ViewData.Phototechnics;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class PhototechnicsController : Controller
    {
        private readonly AppContext _context = new AppContext();
        private IPhototechnicsService _phototechnicsService;

        public PhototechnicsController(IPhototechnicsService phototechnicsService)
        {
            _phototechnicsService = phototechnicsService;
        }

        public ActionResult ShopList(string shortcut)
        {
            var phototechnics = _context.Phototechnicses.SingleOrDefault(x => x.Shortcut == shortcut);
            if (phototechnics == null)
            {
                return HttpNotFound();
            }
            return View(phototechnics);
        }

        public ActionResult Index(int from = 0, int pageSize = 20)
        {
            var data = _phototechnicsService.Where();

            var model = new IndexViewData
            {
                Page = data
            };

            return View(model);
        }

        // GET: /PhototechnicsModel/Details/5
        public async Task<ActionResult> Details(string shortcut)
        {
            if (string.IsNullOrEmpty(shortcut))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phototechnics phototechnics = await _context.Phototechnicses.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (phototechnics == null)
            {
                return HttpNotFound();
            }
            return View(phototechnics);
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