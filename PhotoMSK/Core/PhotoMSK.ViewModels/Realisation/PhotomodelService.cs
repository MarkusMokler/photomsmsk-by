using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PhotomodelService : AbstractService, IPhotomodelService
    {
        public PageView<PhotomodelViewModel.Summary> Where(PageRequest<Photomodel> request = null)
        {
            if (request == null) request = new PageRequest<Photomodel>();

            PageView<PhotomodelViewModel.Summary> page;

            using (MiniProfiler.Step("Loading Context.Photomodel"))
            {
                page = Context.Photomodels
                    .OfType<Photomodel>().Include(y => y.Phones.Select(z => z.Phone))
                    .OrderByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PhotomodelViewModel.Summary>();
            }

            return page;
        }
    }
}