using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Routes;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    class PhotographerService : AbstractService, IPhotographerService
    {
        public PageView<PhotographerViewModel.Summary> Where(PageRequest<Photographer> request = null)
        {
            if (request == null) request = new PageRequest<Photographer>();

            PageView<PhotographerViewModel.Summary> page;

            using (MiniProfiler.Step("Load Context.Photographers"))
            {

                page = Context.Photographers.AsNoTracking().Include(x => x.Raiting)
                    .OrderBy(x => x.Pro)
                    .ThenBy(x => x.Verified)
                    .ThenByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhotographerViewModel.Summary>();
            }

            return page;
        }
    }
}
