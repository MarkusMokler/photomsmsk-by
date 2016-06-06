using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using Event = PhotoMSK.Data.Models.Event;


namespace Fotobel.Api.Services
{
    public class GoogleCalendarController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var time = DateTime.Now.AddMinutes(-30);
            CalendarReference reference;
            using (var context = new AppContext())
            {
                reference = context.CalendarReference.FirstOrDefault(x => x.LastCollectTime < time);
                if (reference == null)
                    return Ok();
                reference.LastCollectTime = DateTime.Now;
                context.SaveChanges();

            }

            CalendarService myService = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA_kHmzh1ywenkfIp7T6O6RuH0PbHgA22c",
                ApplicationName = "Photolab"
            });

            FreebusyResource resource = new FreebusyResource(myService);

            FreeBusyRequest rq = new FreeBusyRequest() { TimeMax = DateTime.Now.AddMonths(1), TimeMin = DateTime.Today.AddDays(-1), Items = new[] { new FreeBusyRequestItem() { Id = reference.CopyFrom } } };

            var any = resource.Query(rq).Execute();

            var list = any.Calendars.Values.First().Busy;

            using (var context = new AppContext())
            {
                reference = context.CalendarReference.Find(reference.ID);
       
                var events = reference.Calendar.Events.Where(x => x.Start > DateTime.Today.AddDays(-1)).ToList();

                var remove = new List<Event>();

                foreach (var ev in events)
                {
                    if (!list.Any(x => x.Start == ev.Start && x.End == ev.End))
                    {
                        context.Entry(ev).State = EntityState.Deleted;
                        remove.Add(ev);
                    }

                }

                foreach (var ev in list)
                {
                    if (!events.Any(x => x.Start == ev.Start.Value && x.End == ev.End.Value))
                    {
                        reference.Calendar.Events.Add(new Event()
                        {
                            ID = Guid.NewGuid(),
                            Start = ev.Start.Value,
                            End = ev.End.Value,
                            UserInformationID = Guid.Parse("00000000-0000-0000-0000-000000000000")
                        });
               
                    }
                }

                context.SaveChanges();
            }



            return Ok();
        }
    }
}
