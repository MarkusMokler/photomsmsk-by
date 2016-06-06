using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PhotoshopService : AbstractService, IPhotoshopService
    {
        public PageView<PhotoshopViewModel.Summary> Where(PageRequest<Photoshop> request = null)
        {
            if (request == null) request = new PageRequest<Photoshop>();

            PageView<PhotoshopViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Photoshop"))
            {
                page = Context.Photoshops.OfType<Photoshop>()
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhotoshopViewModel.Summary>();
            }

            return page;
        }
    }
}