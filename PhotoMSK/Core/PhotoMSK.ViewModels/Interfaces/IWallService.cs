using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IWallService
    {
        IEnumerable<WallPostViewModel.Details> GetWallForRoute(Guid routeID);
        IEnumerable<MainWallViewModel> GetMainWall(Guid? userID = null);
        WallPostViewModel.Details GetDetails(Guid id);

    }
}
