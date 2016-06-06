using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Photostudio
{
    [Authorize]
    public class HallController : ApiController
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            return Ok(new PageView<HallViewModel.Summary>
            {
                Elements = _context.Value.Halls.Where(x => x.Photostudio.Shortcut == shortcut).ToList().As<IList<HallViewModel.Summary>>(),
                PageSize = 20,
                ItemsCount = _context.Value.Halls.Count()
            });
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut, Guid hallId)
        {
            return Ok(_context.Value.Halls.Single(x => x.ID == hallId).As<HallViewModel.Details>());
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, [FromBody] Proxy<PhotoMSK.Data.Models.Hall> model)
        {
            var adminUid = User.GetUserInformationID();
            var route = _context.Value.Photostudios.Single(x => x.Shortcut == shortcut);
            if (
                !route.Participants.Any(
                    z =>
                        z.AccessStatus == AccessStatus.Owner ||
                        z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);


            var hall = new PhotoMSK.Data.Models.Hall()
            {
                Calendar = new HallCalendar(),
                Schedule = new Collection<ScheduleDay>
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
                }
            };

            model.Patch(hall);
            hall.ID = Guid.NewGuid();
            hall.Photostudio = route;

            _context.Value.Halls.Add(hall);
            _context.Value.SaveChanges();

            return Ok(hall.As<HallViewModel.Details>());

        }

        [HttpPut]
        [Message("Настройки обновлены")]
        public IHttpActionResult Put(string shortcut, Guid id, [FromBody] PhotoMSK.Data.Models.Hall @hallUpd)
        {
            var adminUid = User.GetUserInformationID();
            var route = _context.Value.Photostudios.Single(x => x.Shortcut == shortcut);
            if (
                !route.Participants.Any(
                    z =>
                        z.AccessStatus == AccessStatus.Owner ||
                        z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);


            var hall = _context.Value.Halls.Find(id);

            hall.HallType = @hallUpd.HallType;
            hall.Description = @hallUpd.Description;
            hall.HallProperties = @hallUpd.HallProperties;
            hall.Name = @hallUpd.Name;
            hall.Photos = @hallUpd.Photos;
            hall.Square = @hallUpd.Square;
            hall.TeaserImage = @hallUpd.TeaserImage;


            _context.Value.SaveChanges();
            return Ok(hall.As<HallViewModel.Details>());
        }

        [HttpDelete]
        [Message("Зал успешно удален")]
        public IHttpActionResult Delete(string shortcut, Guid id)
        {
            var adminUid = User.GetUserInformationID();
            var route = _context.Value.Photostudios.Single(x => x.Shortcut == shortcut);
            if (
                !route.Participants.Any(
                    z =>
                        z.AccessStatus == AccessStatus.Owner ||
                        z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);

            var hall = _context.Value.Halls.Find(id);
            _context.Value.Entry(hall).State = EntityState.Deleted;
            _context.Value.SaveChanges();
            return Ok();
        }
    }
}
