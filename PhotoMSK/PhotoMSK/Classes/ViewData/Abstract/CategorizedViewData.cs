using System.Collections.Generic;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData
{
    public interface CategorizedViewData : IBaseViewData
    {
        IList<CategoryViewModel> Categorieses { get; set; }
    }
}