using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{
    /// <summary>
    /// Workin with calendar's data.
    /// </summary>
    [Authorize]
    public class RentCalendarController : ApiController
    {
        readonly AppContext _context = new AppContext();
        private readonly IPhototechnicsService _phototechnicsService;

        /// <summary>
        /// Construct new instance.
        /// </summary>
        /// <param name="phototechnicsService"></param>
        public RentCalendarController(IPhototechnicsService phototechnicsService)
        {
            _phototechnicsService = phototechnicsService;
        }


        [HttpGet]
        public PageView<PhototechnicsViewModel.Summary> Get(string shortcut, int? take, int skip, int page = 0, int pageSize = 20)
        {
            var elem = _context.Routes
                .Include(x => x.Raiting)
                .Single(x => x.Shortcut == shortcut);

            return _phototechnicsService
                .GetPhotorentTechnics(elem.As<PhotorentViewModel.Summary>(),
                new PageRequest<RentCalendar>()
                {
                    Page = page,
                    Size = pageSize,
                    Where = x => x.PhotorentID == elem.ID
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortcut">shortcut to route</param>
        /// <param name="group"></param>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="take"></param>
        /// <returns>page with data</returns>
        [HttpGet]
        public PageView<PhototechnicsViewModel.Summary> Get(string shortcut, string group = null, string sort = null, string filter = null, int take = 20, int skip = 0, int page = 0, int pageSize = 20)
        {
            var elem = _context.Routes
                .Include(x => x.Raiting)
                .Single(x => x.Shortcut == shortcut);

            return _phototechnicsService
                .GetPhotorentTechnics(elem.As<PhotorentViewModel.Summary>(),
                new PageRequest<RentCalendar>()
                {
                    Page = page,
                    Size = pageSize,
                    Where = x => x.PhotorentID == elem.ID
                });

        }

        /// <summary>
        /// Return single item 
        /// </summary>
        /// <param name="shortcut">Shortcut to route</param>
        /// <param name="id">id of calendar</param>
        /// <returns></returns>
        [HttpGet]
        public PhototechnicsViewModel.Summary Get(string shortcut, Guid id)
        {
            return _context.RentCalendars.Find(id).As<PhototechnicsViewModel.Summary>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortcut">Shortcut to route</param>
        /// <param name="value">POST data from results;</param>
        [HttpPost]
        public void Post(string shortcut, [FromBody]Proxy<RentCalendar> value)
        {
            var rent = _context.Photorents.SingleOrDefault(x => x.Shortcut == shortcut);
            Debug.Assert(rent != null, "rent != null");
            RentCalendar calendar = new RentCalendar();
            value.Patch(calendar);
            calendar.ID = Guid.NewGuid();
            rent.AddCalendar(calendar);
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of calendar</param>
        /// <param name="value">POST data from results;</param>
        [HttpPut]
        public void Put(Guid id, [FromBody]Proxy<RentCalendar> value)
        {
            RentCalendar calendar = _context.RentCalendars.Find(id);
            value.Patch(calendar);
            _context.SaveChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id of calendar</param>
        [HttpDelete]
        public void Delete(Guid id)
        {
            RentCalendar calendar = _context.RentCalendars.Find(id);
            _context.Entry(calendar).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
