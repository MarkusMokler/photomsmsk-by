using System;
using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PhototechnicsService : AbstractService, IPhototechnicsService
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());
        private readonly IUrlBuilder _urlBuilder;

        public PhototechnicsService(IUrlBuilder urlBuilder)
        {
            _urlBuilder = urlBuilder;
        }

        public PageView<PhototechnicsViewModel.Summary> GetPhotoshopTechnics(RouteEntityViewModel.Summary route, PageRequest<PricePosition> request)
        {
            PageView<PhototechnicsViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Phototechnics"))
            {
                page = Context.PricePositions
                    .Include(x => x.Phototechnics)
                    .Include(x => x.Phototechnics.Category)
                    .OrderBy(x => x.Phototechnics.Raiting.Total)
                    .ToPage(request).AsPageView<PhototechnicsViewModel.Summary>();
            }

            foreach (var element in page.Elements)
            {
                if (route.WhiteLabel)
                {
                    element.Shortcut = _urlBuilder.GetRouteForObject(new RouteObject
                    {
                        CategorySlug = element.CategorySlug,
                        Domain = route.Domain,
                        RouteShortcut = route.Shortcut,
                        Shortcut = element.Shortcut,
                        WhiteLabel = route.WhiteLabel
                    });
                }

            }

            return page;
        }

        public PageView<PhototechnicsViewModel.Summary> GetPhotorentTechnics(RouteEntityViewModel.Summary route, PageRequest<RentCalendar> request)
        {
            PageView<PhototechnicsViewModel.Summary> page;
            using (MiniProfiler.Step("Loading Context.Phototechnics"))
            {
                page = _context.Value.RentCalendars
                    .Include(x => x.Phototechnics)
                    .Include(x => x.Photorent)
                    .Include(x => x.Phototechnics.Raiting)
                    .Include(x => x.Phototechnics.Category)
                    .Include(x => x.Phototechnics.Brand)
                    .OrderBy(x => x.Phototechnics.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhototechnicsViewModel.Summary>();
            }

            foreach (var element in page.Elements)
            {
                if (route.WhiteLabel)
                {
                    element.Shortcut = _urlBuilder.GetRouteForObject(new RouteObject
                    {
                        CategorySlug = element.CategorySlug,
                        Domain = route.Domain,
                        RouteShortcut = route.Shortcut,
                        Shortcut = element.Shortcut,
                        WhiteLabel = route.WhiteLabel
                    });
                }

            }
            return page;
        }

        public PageView<PhototechnicsViewModel.Details> Where(PageRequest<Phototechnics> request = null)
        {
            if (request == null) request = new PageRequest<Phototechnics>();

            PageView<PhototechnicsViewModel.Details> page;

            using (MiniProfiler.Step("Loading Context.Phototechnics"))
            {
                page = Context.Phototechnicses
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhototechnicsViewModel.Details>();
            }

            return page;
        }

        public PageView<PhototechnicsViewModel.Summary> WhereSummary(PageRequest<Phototechnics> request = null)
        {
            if (request == null) request = new PageRequest<Phototechnics>();

            PageView<PhototechnicsViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Phototechnics"))
            {
                page = Context.Phototechnicses
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhototechnicsViewModel.Summary>();
            }

            return page;
        }
    }
}