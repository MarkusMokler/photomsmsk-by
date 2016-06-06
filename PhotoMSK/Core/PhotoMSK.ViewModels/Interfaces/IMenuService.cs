using System;
using System.Collections.Generic;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IMenuService
    {
        IList<MenuItemViewModel> GetMenuForRoute(Guid RouteID);
    }
}