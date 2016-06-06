using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    class PhotostudioService : AbstractService, IPhotostudioService
    {
      
        public PageView<PhotostudioViewModel.Summary> Where(PageRequest<Photostudio> request = null)
        {
            if (request == null) request = new PageRequest<Photostudio>();

            PageView<PhotostudioViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Photostudios"))
            {
                page = Context.Photostudios
                    .OfType<Photostudio>()
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhotostudioViewModel.Summary>();
            }

            using (MiniProfiler.Step("Loading Context.RoutesPhones"))
            {
                page.Elements
                    .GetReferenses(x => x.ID, Context.RoutesPhones.Include(x => x.Phone), x => x.EntityID)
                    .As<IList<PhoneViewModel.Summary>>()
                    .AttachListTo(page.Elements, x => x.EntityID, x => x.ID);
            }

            using (MiniProfiler.Step("Loading Context.Halls"))
            {
                page.Elements
                    .GetReferenses(x => x.ID, Context.Halls.Include(x => x.Schedule), x => x.PhotostudioID)
                    .As<IList<HallViewModel.Descriptor>>()
                    .AttachListTo(page.Elements, x => x.PhotostudioID, x => x.ID);
            }
            
            return page;

        }
    }
}
