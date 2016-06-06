using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhotographerService : IService
    {
        PageView<PhotographerViewModel.Summary> Where(PageRequest<Photographer> request = null);

    }
}
