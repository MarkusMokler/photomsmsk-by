using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhototechnicsService : IService
    {
        PageView<PhototechnicsViewModel.Summary> GetPhotoshopTechnics(RouteEntityViewModel.Summary route, PageRequest<PricePosition> request = null);
        PageView<PhototechnicsViewModel.Summary> GetPhotorentTechnics(RouteEntityViewModel.Summary route, PageRequest<RentCalendar> request = null);
        PageView<PhototechnicsViewModel.Details> Where(PageRequest<Phototechnics> request = null);
        PageView<PhototechnicsViewModel.Summary> WhereSummary(PageRequest<Phototechnics> request = null);

    }
}
