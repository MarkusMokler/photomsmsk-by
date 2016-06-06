using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PlaceService : AbstractService, IPlaceService
    {
        public PageView<PlaceViewModel.Summary> Where(PageRequest<Place> request = null)
        {
            if (request == null) request = new PageRequest<Place>();

            PageView<PlaceViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Place"))
            {
                page = Context.Places
                    .OfType<Place>()
                    .OrderByDescending(x => x.Raiting.Total)                    
                    .ToPage(request)
                    .AsPageView<PlaceViewModel.Summary>();
            }

            return page;
        }
    }
}