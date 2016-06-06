using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Social;
using PhotoMSK.ViewModels.Widgets;

namespace PhotoMSK.ViewModels.RoutePageLayouts
{
    public class ZoneViewModel
    {
        public string Name { get; set; }
        public virtual List<BaseWidgetViewModel.Details> Widgets { get; set; }
    }

}
