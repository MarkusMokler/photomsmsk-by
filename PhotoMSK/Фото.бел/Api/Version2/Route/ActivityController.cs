using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Activity;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class ActivityController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public PageView<ActivityViewModel.Summary> Get(string shortcut, int page = 0, int size = 20)
        {
            var activies = _context.Activities.Where(x => x.Route.Shortcut == shortcut)
                .OrderByDescending(x => x.ActivityTime)
                .Skip(page * size)
                .Take(size)
                .ToList();
            return new PageView<ActivityViewModel.Summary>
            {
                Elements = GetElement(activies).ToList(),
                ItemsCount = _context.Activities.Count(x => x.Route.Shortcut == shortcut),
                PageSize = size
            };
        }

        public static IEnumerable<ActivityViewModel.Summary> GetElement(List<Activity> activies)
        {
            foreach (var activity in activies)
            {
                var eventActivity = activity as EventActivity;
                if (eventActivity != null)
                    yield return AutoMapper.Mapper.Map<EventActivityViewModel.Summary>(eventActivity);
                else

                {
                    var callActivity = activity as CallActivity;

                    if (callActivity != null)
                        yield return AutoMapper.Mapper.Map<CallActivityViewModel.Summary>(callActivity);
                    else
                    {
                        yield return activity.As<ActivityViewModel.Summary>();
                    }
                }

            }
        }
    }
}
