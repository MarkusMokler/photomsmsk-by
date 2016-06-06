using System.Collections.Generic;
using System.Web.Mvc;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Home
{
    public class WhiteLabelNewsViewData : WhiteLabelViewData
    {
        public IList<WallPostViewModel.Details> Wall { get; set; }
     }
}