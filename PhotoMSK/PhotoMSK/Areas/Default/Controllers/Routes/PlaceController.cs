using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PhotoMSK.Areas.Default.ViewData.Place;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.Areas.Default.Controllers
{
    public partial class PlaceController : Controller
    {
        private readonly AppContext db = new AppContext();
        private IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        // GET: /PhototechnicsModel/
        public async Task<ActionResult> Index()
        {
            var data = _placeService.Where();

            var model = new IndexViewData()
            {
                Page = data
            };

            return View(model);
        }

        // GET: /PhototechnicsModel/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Place place = await db.Places.FindAsync(id);

            if (place == null)
                return HttpNotFound();

            /*var model = new DetailsViewDate
            {
                Item = Mapper.Map<PlaceViewModel.Details>(place)
            };*/

            return View(place);
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