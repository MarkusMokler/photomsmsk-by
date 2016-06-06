using System;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IPhotostudioService : IService
    {
        PageView<PhotostudioViewModel.Summary> Where(PageRequest<Photostudio> request = null);

    }

    public interface IService : IDisposable
    {
    }
}
