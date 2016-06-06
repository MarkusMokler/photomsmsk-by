using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Realisation
{
    class MenuService : AbstractService, IMenuService
    {

        public IList<MenuItemViewModel> GetMenuForRoute(Guid RouteID)
        {
            return Context.AbstractMenuItem.Where(x => x.RouteID == RouteID).As<IList<MenuItemViewModel>>();
        }


    }
 
}
