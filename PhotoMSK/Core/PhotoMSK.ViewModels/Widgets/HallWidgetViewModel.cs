using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class HallWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "hallWidget";

            public virtual Guid HallID { get; set; }

            public virtual HallViewModel.Details Hall { get; set; }
        }
    }
}
