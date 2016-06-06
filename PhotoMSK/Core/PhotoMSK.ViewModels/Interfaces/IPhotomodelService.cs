using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhotomodelService : IService
    {
        PageView<PhotomodelViewModel.Summary> Where(PageRequest<Photomodel> request = null);
    }
}