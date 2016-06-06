using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPlaceService : IService
    {
        PageView<PlaceViewModel.Summary> Where(PageRequest<Place> request = null);
    }
}