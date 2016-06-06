using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPageService : IService
    {
        PageView<PublicViewModel.Summary> Where(PageRequest<Page> request = null);
    }
}