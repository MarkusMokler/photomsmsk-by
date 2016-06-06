using System.Collections.Generic;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Catalog
{
    public class CartViewData: WhiteLabelViewData
    {
        public IList<OrderLineViewModel> Orders { get; set; }
        public PageView<PhototechnicsViewModel.Summary> Technics { get; set; }
    }

    public class OrderLineViewModel
    {
        public int Price { get; set; }
        public string Image { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }
        public int UnitCount { get; set; }
    }
}