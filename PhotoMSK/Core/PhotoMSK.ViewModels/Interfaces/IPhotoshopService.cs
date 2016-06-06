using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhotoshopService : IService
    {
        PageView<PhotoshopViewModel.Summary> Where(PageRequest<Photoshop> request = null);
    }
}