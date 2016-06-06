using System.Collections.Generic;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Home
{
    public class IndexViewData : BaseViewData
    {
        public IList<MainWallViewModel> MainWall { get; set; }
        public IList<MasterclassViewModel.Summary> Masterclasses { get; set; } 
    }
}