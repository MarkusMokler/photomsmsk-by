using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhotorentService : IService
    {
        PageView<PhotorentViewModel.Summary> Where(PageRequest<Photorent> request = null);
    }
}