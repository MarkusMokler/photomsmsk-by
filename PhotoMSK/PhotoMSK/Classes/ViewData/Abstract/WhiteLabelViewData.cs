using System.Collections.Generic;
using System.Web.Mvc;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData
{

    public abstract class WhiteLabelViewData : BaseViewData, CategorizedViewData
    {
        public virtual MvcHtmlString RouteID { get { return new MvcHtmlString(Route.ID.ToString()); } }
        public virtual  IList<MenuItemViewModel> Menu { get; set; }
        //Dictionary<Zone,Widget> Widgets= new Dictionary<Zone,Widget>(); 
        public virtual IList<CategoryViewModel> Categorieses { get; set; }
        public virtual RouteEntityViewModel.Summary Route { get; set; }
    }
}