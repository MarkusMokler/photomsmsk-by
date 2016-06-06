using System.Collections.Generic;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IMasterclassService : IService
    {
        PageView<MasterclassViewModel.Summary> Where(PageRequest<Masterclass> request = null);
        IList<MasterclassViewModel.Summary> GetNearestMasterclasses();
    }
}