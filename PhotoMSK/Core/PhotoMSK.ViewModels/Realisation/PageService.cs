using System.Data.Entity;
using System.Linq;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;
using StackExchange.Profiling;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PageService : AbstractService, IPageService
    {
        public PageView<PublicViewModel.Summary> Where(PageRequest<Page> request = null)
        {
            if (request == null) request = new PageRequest<Page>();

            PageView<PublicViewModel.Summary> page;

            using (MiniProfiler.Step("Load Context.Pages"))
            {
                page = Context.Pages.Include(x => x.Phones.Select(y => y.Phone)).OfType<Page>()
                    .OrderBy(x => x.Pro)
                    .ThenBy(x => x.Verified)
                    .ThenByDescending(x => x.Raiting.Total)
                    .ToPage(request)
                    .AsPageView<PublicViewModel.Summary>();
            }

            return page;
        }
    }
}