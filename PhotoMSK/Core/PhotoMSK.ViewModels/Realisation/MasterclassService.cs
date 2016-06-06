using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class MasterclassService : AbstractService, IMasterclassService
    {
        public PageView<MasterclassViewModel.Summary> Where(PageRequest<Masterclass> request = null)
        {
            if (request == null) request = new PageRequest<Masterclass>();

            PageView<MasterclassViewModel.Summary> page;

            using (MiniProfiler.Step("Load Context.Masterclass"))
            {
                var startdate = DateTime.Today.AddDays(-2);

                page = Context.Masterclasses
                    .Where(x => x.Start > startdate)
                    .OfType<Masterclass>()
                    .OrderBy(x => x.Pro)
                    .ThenBy(x => x.Verified)
                    .ThenByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<MasterclassViewModel.Summary>();
            }

            return page;
        }

        public IList<MasterclassViewModel.Summary> GetNearestMasterclasses()
        {
            IList<MasterclassViewModel.Summary> masterclasses;

            using (MiniProfiler.Step("Load nearest masterclasses"))
            {
                var startdate = DateTime.Today.AddDays(-2);

                masterclasses = Context.Masterclasses
                .Include(x => x.Phones.Select(y => y.Phone))
                .Where(x => x.Start < startdate)
                .OrderByDescending(x => x.Start)
                .Take(8)
                .ToList()
                .As<IList<MasterclassViewModel.Summary>>();              
            }

            return masterclasses;
        }
    }
}