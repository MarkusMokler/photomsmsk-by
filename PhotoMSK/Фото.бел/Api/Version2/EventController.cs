using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using Fotobel.Api.Version2.Route;
using Fotobel.Classes;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.Permission;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2
{
    public class EventController : ApiController
    {
        private readonly AppContext _context = new AppContext();
        private readonly IEventLocker _eventLocker;

        public EventController(IEventLocker eventLocker)
        {
            _eventLocker = eventLocker;
        }

        [HttpGet]
        [AllowAnonymous]
        public IList<IEventViewModel> Get([FromUri] Guid calendarId, [FromUri]DateTime start, [FromUri]DateTime end)
        {
            var events =
                _context.Events.Where(
                    x =>
                        x.Start > start && x.Start < end && x.Calendar.ID == calendarId &&
                        x.EventState != EventState.Deleted).ToList();

            var eventsColor = _context.Calendars.Find(calendarId).Color;

            foreach (var item in events)
            {
                item.BackgroundColor = eventsColor;
                item.Color = eventsColor;
                item.TextColor = "Black";
            }

            if (User.Identity.IsAuthenticated == false)
            {
                var lockers = _eventLocker.GetLockers(calendarId).Where(x => x.Start > start && x.Start < end).Select(x => new UserEventViewModel
                {
                    Start = x.Start,
                    End = x.End,
                    Title = x.Title,
                    BackgroundColor = eventsColor,
                    Color = eventsColor,
                    TextColor = "Black"
                });
                var ll = lockers;
                var eevents = events.As<List<UserEventViewModel>>();
                eevents.AddRange(ll);
                eevents = eevents.OrderBy(x => x.Start).ToList();

                if (eevents.Any())
                {
                    var tmpevents = new List<UserEventViewModel> { eevents.First() };
                    foreach (var e in eevents.Skip(1))
                    {
                        if (tmpevents.Last().End == e.Start)
                            tmpevents.Last().End = e.End;
                        else
                        {
                            tmpevents.Add(e);
                        }
                    }
                    eevents = tmpevents;
                }



                return eevents.ToList<IEventViewModel>();
            }


            var claendar = _context.Calendars.Find(calendarId);

            var role = claendar.RouteEntity.IsActionPermited(EventManage.ViewAdminEvent, User.GetUserInformationID());

            if (role == false)
            {
                var lockers = _eventLocker.GetLockers(calendarId).Where(x => x.Start > start && x.Start < end).Select(x => new UserEventViewModel()
                {
                    Start = x.Start,
                    End = x.End,
                    Title = x.Title,
                    BackgroundColor = eventsColor,
                    Color = eventsColor,
                    TextColor = "Black"
                });
                var ll = lockers.ToList<IEventViewModel>();
                var eevents = events.As<IList<UserEventViewModel>>().ToList<IEventViewModel>();
                eevents.AddRange(ll);
                return eevents;
            }

            events =
                    _context.Events.Include(x => x.User)
                        .Include(x => x.User.Phones)
                        .Where(
                            x =>
                                x.Start > start && x.Start < end && x.Calendar.ID == calendarId &&
                                x.EventState != EventState.Deleted)
                        .ToList();

            foreach (var item in events)
            {
                item.BackgroundColor = eventsColor;
                item.Color = eventsColor;
                item.TextColor = "Black";
            }

            var llockers = _eventLocker.GetLockers(calendarId).Where(x => x.Start > start && x.Start < end).Select(x => new UserEventViewModel()
            {
                Start = x.Start,
                End = x.End,
                Title = x.Title,
                BackgroundColor = eventsColor,
                Color = eventsColor,
                TextColor = "Black"
            });
            var lll = llockers.ToList<IEventViewModel>();
            var items = events.As<IList<AdminEventViewModel>>().ToList<IEventViewModel>();
            items.AddRange(lll);
            return items;
        }

        public IHttpActionResult Get(Guid id)
        {
            var eventInfo = _context.Events.Find(id);

            if (User.Identity.IsAuthenticated == false)
                return Ok(eventInfo.As<UserEventViewModel>());

            var role = eventInfo.Calendar.RouteEntity.IsActionPermited(EventManage.ViewAdminEvent, User.GetUserInformationID());

            if (role == false)
            {
                return Ok(eventInfo.As<UserEventViewModel>());
            }
            else
            {
                return Ok(eventInfo.As<AdminEventViewModel>());
            }
        }

        [Authorize]
        public IHttpActionResult Get(Guid id, bool activity)
        {
            var eventInfo = _context.Events.Find(id);
            var uid = User.GetUserInformationID();
            var role = eventInfo.Calendar.RouteEntity.IsActionPermited(EventManage.ViewAdminEvent, uid);

            if (role == false)
                throw new AccessValidationExcption();

            var list = _context
                .EventActivities.Where(x => x.EventID == id)
                .ToList();

            var list1 = _context
                .CallActivities.Where(x => x.EventID == id)
                .ToList();

            var lst = ActivityController.GetElement(list.Cast<Activity>().ToList()).ToList();
            lst.AddRange(ActivityController.GetElement(list1.Cast<Activity>().ToList()));


            return Ok(lst.OrderByDescending(x => x.ActivityTime));
        }

        // TODO: Запилить event details для ctrl+click
        /*[Authorize]
        public IHttpActionResult Get(Guid id)
        {
            var eventInfo = _context.Events.Find(id);
            var uid = User.GetUserInformationID();
            var role = eventInfo.Calendar.RouteEntity.IsActionPermited(EventManage.ViewAdminEvent, uid);

            if (role == false)
                throw new AccessValidationExcption();



            var list = _context
                .EventActivities.Where(x => x.EventID == id)
                .ToList();

            var list1 = _context
                .CallActivities.Where(x => x.EventID == id)
                .ToList();

            var lst = ActivityController.GetElement(list.Cast<Activity>().ToList()).ToList();
            lst.AddRange(ActivityController.GetElement(list1.Cast<Activity>().ToList()));


            return Ok(lst.OrderByDescending(x => x.ActivityTime));
        }*/

        [HttpGet]
        public IList<IEventViewModel> GetForRoute(string shortcut, [FromUri]DateTime start, [FromUri]DateTime end)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var uid = User.GetUserInformationID();
            var role = route.IsActionPermited(EventManage.ViewAdminEvent, uid);

            if (role == false)
                throw new AccessValidationExcption();


            if (route is PhotoMSK.Data.Models.Routes.Photostudio)
            {
                var photostudio = route as PhotoMSK.Data.Models.Routes.Photostudio;
                var calendars = photostudio.Halls.Select(x => x.Calendar.ID);

                var events =
                    _context.Events
                        .Include(x => x.User)
                        .Include(x => x.User.Phones)
                        .Where(x => x.Start > start && x.Start < end && calendars.Contains(x.Calendar.ID) && x.EventState != EventState.Deleted)
                        .ToList();

                return events.OrderBy(x => x.Start).As<IList<AdminEventViewModel>>().ToList<IEventViewModel>();

            }
            throw new NotImplementedException();
        }

        [HttpPost]
        public IHttpActionResult Get(ValidateModel model)
        {
            _eventLocker.ClearLocks();
            var uid = User.GetUserInformationID();
            var guids = model.calendarIDs.Where(cid => _eventLocker.Lock(cid, model.start, model.end, uid)).ToList();

            if (model.validate == false)
                return InvalidateEvent(model.start, model.end, guids);

            if (model.start < DateTime.Now.AddHours(1))
                throw new ValidationException("Не возможно забронировать раньше чем завтра");
            var userid = User.Identity.GetUserId();

            var user = _context.Users.Find(userid);

            if (user.UserInformation.Penalties.Any())
                throw new ValidationException("Немозможно забронировать, имеются претензии", MessageCodes.OkAction);

            var calendars = _context.Calendars.Where(x => guids.Contains(x.ID)).ToList();

            var vcals = calendars.Where(x => x.CanAdd(model.start, model.end)).ToList();

            return Ok(
               vcals.Select(x => new { Start = model.start, End = model.end, Summ = x.GetCost(model.start, model.end), Title = x.GetName(), Name = x.GetRouteName(), CalendarID = x.ID }).ToList());
        }

        private IHttpActionResult InvalidateEvent(DateTime start, DateTime end, IEnumerable<Guid> calendarIDs)
        {
            var calendars = _context.Calendars.Where(x => calendarIDs.Contains(x.ID)).ToList();

            return Ok(calendars.Select(x => new { Start = start, End = end, Summ = x.GetCost(start, end), Title = x.GetName(), Name = x.GetRouteName(), CalendarID = x.ID }).ToList());

        }

        [HttpPut]
        [Message("Событие обновлено.")]
        public IHttpActionResult Put([FromUri]Guid id, [FromBody]SaveEventViewModel @event)
        {
            var ee = _context.Events.Find(id);


            if (!ee.Calendar.RouteEntity.IsActionPermited(EventManage.MoveAdminEvent, User.GetUserInformationID()))
                throw new AccessValidationExcption();

            ee.Start = @event.Start;
            ee.End = @event.End;
            ee.Description = @event.Description;
            ee.Tags = @event.Tags;
            ee.PrePayed = @event.PrePayed;
            ee.Price = (double)@event.Price;

            var eventStateChanged = false;

            if (@event.EventState != ee.EventState)
            {
                eventStateChanged = true;
                ee.EventState = @event.EventState;
            }

            if (@event.EventState != EventState.PayByCash && @event.EventState != EventState.PayByCard &&
                @event.EventState != EventState.Missed && @event.EventState != EventState.NotSet && eventStateChanged)
            {
                var msg =
               $"Ваша бронь изменена в {ee.Calendar.GetName()}, {ee.Calendar.GetRouteName()} на {ee.Start.Day:00}.{ee.Start.Month:00} новое время: {ee.Start.Hour:00}:{ee.Start.Minute:00} по {ee.End.Hour:00}:{ee.End.Minute:00}";

                EventActivity activity = new EventActivity()
                {
                    ID = Guid.NewGuid(),
                    Event = ee,
                    UserID = User.GetUserInformationID(),
                    Title = "Перенос брони",
                    Description = msg,
                    ActivityTime = DateTime.Now,
                    Route = ee.Calendar.RouteEntity,
                    State = EventActivityState.Moved
                };
                _context.Activities.Add(activity);

                _context.SmsMessages.Add(
                    new SmsMessage { ID = Guid.NewGuid(), Message = msg, Phone = ee.User.Phones.First().Phone.Number });
            }
            else
            {
                var activityState = EventActivityState.Moved;
                var status = "";
                var summStat = " Сумма: " + @event.Price;
                switch (@event.EventState)
                {
                    case EventState.PayByCash:
                        status = "наличными." + summStat;
                        activityState = EventActivityState.Payed;
                        break;

                    case EventState.PayByCard:
                        status = "по карте (безнал)." + summStat;
                        activityState = EventActivityState.Payed;
                        break;

                    case EventState.Missed:
                        status = "не произошла." + summStat;
                        activityState = EventActivityState.Missed;
                        break;

                    case EventState.NotSet:
                        status = "отменена. Клиент направлен в 'Активность'.";
                        break;
                }

                var msg = "Оплата " + status;

                EventActivity activity = new EventActivity()
                {
                    ID = Guid.NewGuid(),
                    Event = ee,
                    UserID = User.GetUserInformationID(),
                    Title = "Обработка оплаты",
                    Description = msg,
                    ActivityTime = DateTime.Now,
                    Route = ee.Calendar.RouteEntity,
                    State = activityState,
                };

                _context.Activities.Add(activity);
            }

            _context.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpPatch]
        public IHttpActionResult Patch([FromUri] Guid id, List<JsonPatchElement> list)
        {

            var emp = _context.Events.Find(id);
            var uid = User.GetUserInformationID();

            var route = emp.Calendar.RouteEntity;

            if (!route.IsActionPermited(EventManage.FullPatch, uid))
                throw new AccessViolationException();

            if (emp != null)
            {
                // read from request dto properties
                var properties = emp.GetType().GetProperties();

                // update values which are specified to update only
                foreach (var op in list)
                {
                    var fieldName = op.Path.Replace("/", "").ToLower(); // assume leading /slash only for example

                    // patch field is in type
                    if (properties.Count(x => x.Name.ToLower() == fieldName) <= 0) continue;
                    {
                        var persistentProperty = properties.ToList().First(x => x.Name.ToLower() == fieldName);

                        // update property on persistent object
                        // i'm sure this can be improved, but you get the idea...
                        if (persistentProperty.PropertyType == typeof(string))
                        {
                            persistentProperty.SetValue(emp, op.Value, null);
                        }
                        else if (persistentProperty.PropertyType == typeof(int))
                        {
                            int valInt = 0;
                            if (Int32.TryParse(op.Value, out valInt))
                            {
                                persistentProperty.SetValue(emp, valInt, null);
                            }
                        }
                        else if (persistentProperty.PropertyType == typeof(int?))
                        {
                            int valInt = 0;
                            if (op.Value == null)
                            {
                                persistentProperty.SetValue(emp, null, null);
                            }
                            else if (Int32.TryParse(op.Value, out valInt))
                            {
                                persistentProperty.SetValue(emp, valInt, null);
                            }
                        }
                        else if (persistentProperty.PropertyType == typeof(DateTime))
                        {
                            var valDt = default(DateTime);
                            if (DateTime.TryParse(op.Value, out valDt))
                            {
                                persistentProperty.SetValue(emp, valDt, null);
                            }
                        }
                        if (persistentProperty.PropertyType.IsEnum)
                            persistentProperty.SetValue(emp, Proxy<object>.ParseEnum(persistentProperty.PropertyType, op.Value), null);
                    }
                }

                // update
                _context.SaveChanges();
            }


            return Ok();
        }


        [HttpDelete]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(Guid id, bool isPenaltySet = false)
        {
            Event @event = _context.Events.Find(id);

            if (@event == null)
                throw new ValidationException("Не найдена бронь для отмены", MessageCodes.OkAction);

            if (@event.Start < DateTime.Now)
                throw new ValidationException("Не возможно отменить прошедшую аренду", MessageCodes.OkAction);

            var userid = User.GetUserInformationID();

            var inrole = @event.Calendar.RouteEntity.IsActionPermited(EventManage.DeleteAdminEvent, userid);

            if (!inrole)
                throw new AccessValidationExcption();

            var doc = @event.Calendar.RouteEntity.As<PhotoMSK.Data.Models.Routes.Photostudio>().DaysOfClaim;
            var dote = Math.Abs(DateTime.Now.Subtract(@event.Start).Days);
            if (doc > dote && !isPenaltySet)
            {

                var calendar = @event.Calendar;
                var penaltyOpt = new { isPenalty = true, Summ = calendar.GetCost(@event.Start, @event.End), Title = calendar.GetName(), Name = calendar.GetRouteName(), CalendarID = calendar.ID };
                return Ok(penaltyOpt);
            }

            var msg =
                $"Бронь отменена на {@event.Start.Day:00}.{@event.Start.Month:00} c {@event.Start.Hour:00}:{@event.Start.Minute:00} по {@event.End.Hour:00}:{@event.End.Minute:00}";

            var activity = new EventActivity()
            {
                ID = Guid.NewGuid(),
                Event = @event,
                UserID = userid,
                Title = "Отмена брони",
                Description = msg,
                ActivityTime = DateTime.Now,
                Route = @event.Calendar.RouteEntity,
                State = EventActivityState.Removed
            };
            @event.EventState = EventState.Deleted;
            _context.Activities.Add(activity);

            _context.SaveChanges();
            return Ok();
        }
    }

    public class ValidateModel
    {
        public bool validate { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Guid[] calendarIDs { get; set; }
    }
}