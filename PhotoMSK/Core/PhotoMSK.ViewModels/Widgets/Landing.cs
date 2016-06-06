using System.Collections.Generic;
using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels.Widgets
{
    public class LandingPageViewModel
    {
        public class Summary : UniqueEntity
        {
        }

        public class Details : UniqueEntity
        {
            public virtual List<BaseWidgetViewModel.Details> Widgets { get; set; }
        }
    }
}