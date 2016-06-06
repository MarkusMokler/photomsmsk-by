using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using Fotobel.Classes;
using HandlebarsDotNet;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class BookingController : ApiController
    {

        private readonly AppContext _context = new AppContext();
        private readonly IEventLocker _eventLocker;

        public BookingController(IEventLocker eventLocker)
        {
            this._eventLocker = eventLocker;
        }

        /// <summary>
        /// Book event in calendar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Message("Забронировано успешно")]
        public IList<SaveEventViewModel> Book(BookingModel model)
        {
            var uid = User.GetUserInformationID();
            _eventLocker.ClearLocks(uid);
            return model.IsAdmin ? AdminBooking(model) : UserBooking(model);
        }

        private static Guid GetUserInformationID(BookingModel model)
        {
            if (model.UserInformation.ID != Guid.Empty)
                return model.UserInformation.ID;

            using (var context = new AppContext())
            {
                var userphone = model.UserInformation.UserPhone.Replace("-", "").Replace(" ", "");
                //TODO validate phone
                var phone = context.Phones.SingleOrDefault(x => x.Number == userphone);

                Guid id;
                if (phone != null)
                {
                    if (phone.UserPhone != null)
                        return phone.UserPhone.User.ID;
                    id = Guid.NewGuid();

                    phone.UserPhone = new UserPhone
                    {
                        ID = id,
                        Confirm = false,
                        DateAdded = DateTime.Now,
                        User = new PhotoMSK.Data.Models.UserInformation
                        {
                            ID = id,
                            FirstName = model.UserInformation.FirstName,
                            LastName = model.UserInformation.LastName,

                        }
                    };
                    context.SaveChanges();
                    return id;
                }

                id = Guid.NewGuid();

                var info = new PhotoMSK.Data.Models.UserInformation
                {
                    ID = id,
                    FirstName = model.UserInformation.FirstName,
                    LastName = model.UserInformation.LastName,
                    Phones = new HashSet<UserPhone>
                    {
                        new UserPhone
                        {
                            ID = id,
                            Confirm = false,
                            DateAdded = DateTime.Now,
                            Phone = new Phone
                            {
                                ID = id,
                                Number = userphone,
                                DateLastSending = DateTime.Now

                            }
                        }
                    }
                };
                context.UserInformations.Add(info);
                context.SaveChanges();
                return id;
            }
        }
        private IList<SaveEventViewModel> UserBooking(BookingModel model)
        {
            Guid uid = User.GetUserInformationID();
            var userinfo = model.UserInformation == null ? _context.UserInformations.Single(x => x.ID == uid) : _context.UserInformations.Single(x => x.ID == model.UserInformation.ID);

            if (userinfo.Penalties.Any())
                throw new ValidationException("Невозможно забронировать студию. Имеются притензии.", MessageCodes.OkAction);

            var ids = model.Events.Select(y => y.CalendarID).ToList();
            var calendars = _context.Calendars.Where(x => ids.Contains(x.ID)).ToList();


            var strs = new List<string>();

            var order = new Order
            {
                ID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                UserInformationID = userinfo.ID,
                Status = _context.Statuses.Find(StatusID.NewOrder) ?? new Status { ID = StatusID.NewOrder, Title = "Новый Заказ", Description = "Толькло что созданный заказ" }
            };

            string helloText = null;
            string endText = null;
            foreach (var @event in model.Events)
            {
                var calendar = calendars.Single(x => x.ID == @event.CalendarID);
                if (!calendar.CanAdd(@event.Start, @event.End)) continue;

                var route = calendar.RouteEntity;
                var stringtemplare = route.Sms?.BookingLineText ?? "зал {{calendarName}} в {{name}} на {{eventStartDay}}.{{eventStartMonth}} c {{eventStartHour}}:{{eventStartMinute}} по {{eventEndHour}}:{{eventEndMinute}}";
                var template = Handlebars.Compile(stringtemplare);

                if (helloText == null)
                    helloText = route.Sms?.BookingHelloText ?? "На ваше имя забронирован ";
                if (endText == null)
                    endText = route.Sms?.BookingEndText ?? "";

                var newevent = new Event { ID = Guid.NewGuid(), Title = "Занято", Start = @event.Start, End = @event.End, AllDay = @event.AllDay, UserInformationID = userinfo.ID, Price = calendar.GetCost(@event.Start, @event.End), Tags = "online", Calendar = calendar };

                order.Add(newevent);

                var data = new
                {
                    calendarName = calendar.GetName(),
                    name = calendar.GetRouteName(),
                    eventStartDay = $"{@event.Start.Day:00}",
                    eventStartMonth = $"{@event.Start.Month:00}",
                    eventStartHour = $"{@event.Start.Hour:00}",
                    eventStartMinute = $"{@event.Start.Minute:00}",
                    eventEndHour = $"{@event.End.Hour:00}",
                    eventEndMinute = $"{@event.End.Minute:00}"
                };

                var str = WebUtility.HtmlDecode(template(data));

                strs.Add(str);


                EventActivity activity = new EventActivity
                {
                    ID = Guid.NewGuid(),
                    Event = newevent,
                    UserID = userinfo.ID,
                    Title = "Бронирование Онлайн",
                    Description = str,
                    ActivityTime = DateTime.Now,
                    Route = calendar.RouteEntity,
                    State = EventActivityState.Created
                };
                _context.Activities.Add(activity);

                if (DateTime.Now.AddDays(1) > @event.Start)
                    _context.SmsMessages.Add(new SmsMessage
                    {
                        ID = Guid.NewGuid(),
                        Message =
                            $"Бронь на {@event.Start.Day:00}.{@event.Start.Month:00} c {@event.Start.Hour:00}:{@event.Start.Minute:00} по {@event.End.Hour:00}:{@event.End.Minute:00}",
                        Phone = calendars.First().RouteEntity.Phones.First(x => x.Confirm).Phone.Number
                    });
            }

            var message = helloText + string.Join(",", strs) + endText;



            _context.SmsMessages.Add(new SmsMessage
            {
                ID = Guid.NewGuid(),
                Message = message,
                Phone = userinfo.Phones.First().Phone.Number
            });

            _context.Orders.Add(order);
            _context.SaveChanges();
            return model.Events;
        }
        private IList<SaveEventViewModel> AdminBooking(BookingModel model)
        {

            var userInformationID = GetUserInformationID(model);
            var ids = model.Events.Select(x => x.CalendarID).ToList();
            var rawcalendars = _context.Calendars.Where(x => ids.Contains(x.ID)).ToList();


            var adminUid = User.GetUserInformationID();
            var calendars =
                rawcalendars.Where(
                    x =>
                        x.RouteEntity.Participants.Any(
                            z =>
                                z.AccessStatus == AccessStatus.Owner ||
                                z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == adminUid))
                    .ToList();

            var strs = new List<string>();

            var order = new Order
            {
                ID = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                UserInformationID = userInformationID,
                Status = _context.Statuses.Find(StatusID.NewOrder) ?? new Status { ID = StatusID.NewOrder, Title = "Новый Заказ", Description = "Толькло что созданный заказ" }
            };


            string helloText = null;
            string endText = null;
            foreach (var @event in model.Events)
            {
                var calendar = calendars.FirstOrDefault(x => x.ID == @event.CalendarID);
                if (calendar == null) continue;

                var route = calendar.RouteEntity;
                var stringtemplare = route.Sms?.BookingLineText ?? "зал {{calendarName}} в {{name}} на {{eventStartDay}}.{{eventStartMonth}} c {{eventStartHour}}:{{eventStartMinute}} по {{eventEndHour}}:{{eventEndMinute}}";
                var template = Handlebars.Compile(stringtemplare);

                if (helloText == null)
                    helloText = route.Sms?.BookingHelloText ?? "На ваше имя забронирован ";
                if (endText == null)
                    endText = route.Sms?.BookingEndText ?? "";

                var newevent = new Event
                {
                    ID = Guid.NewGuid(),
                    Calendar = calendar,
                    Title = "Занято",
                    Start = @event.Start,
                    End = @event.End,
                    AllDay = @event.AllDay,
                    UserInformationID = userInformationID,
                    Description = @event.Description,
                    Price = @event.Price ?? calendar.GetCost(@event.Start, @event.End),
                    Tags = @event.Tags,
                    CreatedByID = adminUid
                };

                order.Add(newevent);


                var data = new
                {
                    calendarName = calendar.GetName(),
                    name = calendar.GetRouteName(),
                    eventStartDay = $"{@event.Start.Day:00}",
                    eventStartMonth = $"{@event.Start.Month:00}",
                    eventStartHour = $"{@event.Start.Hour:00}",
                    eventStartMinute = $"{@event.Start.Minute:00}",
                    eventEndHour = $"{@event.End.Hour:00}",
                    eventEndMinute = $"{@event.End.Minute:00}"
                };

                var str = WebUtility.HtmlDecode(template(data));

                strs.Add(str);

                EventActivity activity = new EventActivity()
                {
                    ID = Guid.NewGuid(),
                    Event = newevent,
                    UserID = adminUid,
                    Title = "Бронирование через админа",
                    Description = str,
                    ActivityTime = DateTime.Now,
                    Route = calendar.RouteEntity,
                    State = EventActivityState.Created
                };
                _context.Activities.Add(activity);
            }

            var message = helloText + string.Join(",", strs) + endText;
            _context.SmsMessages.Add(new SmsMessage { ID = Guid.NewGuid(), Message = message, Phone = model.UserInformation.UserPhone });

            _context.Orders.Add(order);
            _context.SaveChanges();
            return model.Events;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

    }

    public class BookingModel
    {
        /// <summary>
        /// Это поле показывает бронирование идёт от администратора или от клиента
        /// </summary>
        public bool IsAdmin { get; set; }
        public UserInformationViewModel.Summary UserInformation { get; set; }
        public IList<SaveEventViewModel> Events { get; set; }
        public string Error { get; set; }
    }
}