using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2.Route
{
    public class SmsModuleController : ApiController
    {
        readonly AppContext _context = new AppContext();


        public IHttpActionResult Get(string shortcut)
        {
            var li = _context.Routes.Single(x => x.Shortcut == shortcut).Sms;
            return Ok(li == null ? new SmsModuleViewModel.Summary()
            {
                BookingLineText = "зал {{calendarName}} в {{name}} на {{eventStartDay}}.{{eventStartMonth}} c {{eventStartHour}}:{{eventStartMinute}} по {{eventEndHour}}:{{eventEndMinute}}",
                BookingHelloText = "На ваше имя забронированы",
                BookingEndText = ""
            } : li.As<SmsModuleViewModel.Summary>());
        }

        [Authorize]
        public IHttpActionResult Post(string shortcut, SmsModuleViewModel.Summary model)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            if (route.Sms == null)
                route.Sms = new SmsModule
                {
                    ID = Guid.NewGuid()
                };

            route.Sms.BookingLineText = model.BookingLineText;
            route.Sms.BookingHelloText = model.BookingHelloText;
            route.Sms.BookingEndText = model.BookingEndText;

            route.Sms.SmsEnnabled = model.SmsEnnabled;

            _context.SaveChanges();
            return Ok(route.LegalInformation.As<LegalInformationViewModel.Summary>());
        }

    }
}