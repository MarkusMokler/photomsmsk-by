using System.Collections.Generic;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData
{
    public class BasePhototechnicsPageViewData<T> : BasePageViewData<T>
    {
        public IList<CategoryViewModel> Categories { get; set; }
        public IList<BrandViewModel> Brands { get; set; } 
    }
}