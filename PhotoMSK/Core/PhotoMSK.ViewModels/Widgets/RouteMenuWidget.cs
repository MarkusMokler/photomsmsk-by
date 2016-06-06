using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Menu;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class RouteMenuWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "menuWidget";
            public virtual Guid MenuID { get; set; }
            public virtual RouteMenuViewModel.Details Menu { get; set; }
        }

    }
}
