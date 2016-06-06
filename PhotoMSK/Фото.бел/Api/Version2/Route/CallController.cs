using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.ActivityStream;

namespace Fotobel.Api.Version2.Route
{
    [Authorize]
    public class CallController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpPost]
        public IHttpActionResult Post(string shortcut, CallActivityEditModel model)
        {
            CallActivity activity = new CallActivity()
            {
                ID = Guid.NewGuid(),
                ActivityTime = DateTime.Now,
                CallType = CallType.Incoming,
                Start = model.Start ?? DateTime.Now,
                End = model.End ?? DateTime.Now,
                Title = "Входящий звонок",
                Description = model.Description,

                Route = _context.Routes.Single(x => x.Shortcut == shortcut),
                UserID = User.GetUserInformationID(),
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();
            return Ok(activity.ID);
        }

        [HttpPut]
        public IHttpActionResult Put(string shortcut, Guid id, CallActivityEditModel model)
        {
            var activity = _context.Activities.Find(id) as CallActivity;

            if (model.Start.HasValue)
                activity.Start = model.Start.Value;


            if (model.End.HasValue)
                activity.End = model.End.Value;


            if (model.CallType.HasValue)
                activity.CallType = model.CallType.Value;

            if (model.VoiceRecord != null || model.VoiceRecord == "")
                activity.VoiceRecord = model.VoiceRecord;

            _context.SaveChanges();
            return Ok(activity.ID);
        }


    }

    public class CallActivityEditModel
    {
        public string Description { get; set; }
        public DateTime? End { get; set; }
        public DateTime? Start { get; set; }
        public CallType? CallType { get; set; }
        public string VoiceRecord { get; set; }
    }
}
