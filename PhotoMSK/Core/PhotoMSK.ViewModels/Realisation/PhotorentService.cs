using System;
using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PhotorentService : AbstractService, IPhotorentService
    {
        private IPhototechnicsService _phototechnicsService;

        public PhotorentService(IPhototechnicsService phototechnicsService)
        {
            _phototechnicsService = phototechnicsService;
        }

        public PageView<PhotorentViewModel.Summary> Where(PageRequest<Photorent> request = null)
        {
            if (request == null) request = new PageRequest<Photorent>();

            PageView<PhotorentViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Photorent"))
            {
                page = Context.Photorents.OfType<Photorent>()
                    .Include(x => x.Phones.Select(y => y.Phone))
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhotorentViewModel.Summary>();
            }

            return page;
        }

        public PhotorentViewModel.Details GetDetails(string shortcut)
        {
            return Context.Photorents.OfType<Photorent>()
                .Include(x => x.Phones.Select(y => y.Phone)).FirstOrDefault(x => x.Shortcut == shortcut).As<PhotorentViewModel.Details>();
        }

    }
}