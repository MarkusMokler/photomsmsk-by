using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IUrlBuilder
    {
        string GetShortcutUrl(string shortcut);

        string GetRouteForObject(RouteObject oo);
        string GetRoutePhoController(RouteObject oo);

    }
}
