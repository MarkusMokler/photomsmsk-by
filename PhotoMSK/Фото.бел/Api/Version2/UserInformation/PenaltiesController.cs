using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.UserInformation
{
    public class PenaltiesController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get(Guid shortcut)
        {

            var user = _context.UserInformations.FirstOrDefault(x => x.ID == shortcut);
            if (user != null)
            {
                return Ok(user.Penalties.As<IList<PenaltyViewModel.Details>>());
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Guid shortcut, [FromBody] Penalty penalty)
        {

            var user = _context.UserInformations.FirstOrDefault(x => x.ID == shortcut);
            var delPen = user.Penalties.Single(x => x.ID == penalty.ID);
            if (user != null)
            {
                user.Penalties.Remove(delPen);
                _context.Penalties.Remove(delPen);
            }
            /*
            EventActivity activity = new EventActivity()
            {
                ID = Guid.NewGuid(),
                Title = "Претензия Удалена",
                ActivityTime = DateTime.Now,
                Description = "Удалена притензия",
                Event = delPen.Event,
                Route = delPen.Event.Calendar.RouteEntity,
                State = EventActivityState.PenaltyRemove,
                UserID = User.GetUserInformationID()
            };
            _context.Activities.Add(activity);
            */
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Authorize]
        [Message("Претензия отправлена")]
        public IHttpActionResult Post(Guid shortcut, [FromBody]PenaltySaveModel model)
        {

            Guid g = model.EventID;

            var @event = _context.Events.Find(g);

            @event.User.Penalties.Add(new Penalty
            {
                ID = Guid.NewGuid(),
                Event = @event,
                Description = model.Description,
                Price = model.Price,
            });

            var msg =
                $"{@event.Calendar.GetRouteName()} предъявил(а) претензию {model.Description}, суммой {model.Price}";

            _context.SmsMessages.Add(new SmsMessage
            {
                ID = Guid.NewGuid(),
                Phone = @event.User.Phones.First().Phone.Number,
                Message = msg

            });


            var activity = new EventActivity()
            {
                ID = Guid.NewGuid(),
                Event = @event,
                UserID = User.GetUserInformationID(),
                Title = "Добавлена претензия",
                Description = msg,
                ActivityTime = DateTime.Now,
                Route = @event.Calendar.RouteEntity,
                State = EventActivityState.PenaltyAdd
            };

            _context.Activities.Add(activity);

            _context.SaveChanges();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }

    public class PenaltySaveModel
    {
        public Guid EventID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}