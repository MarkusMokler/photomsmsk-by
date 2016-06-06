using System.Collections.Generic;
using System.Web.Mvc;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Catalog
{
    public class ProductViewData : WhiteLabelViewData
    {
        public PhototechnicsViewModel.Details PhototechnicsViewModel { get; set; }
    }
}