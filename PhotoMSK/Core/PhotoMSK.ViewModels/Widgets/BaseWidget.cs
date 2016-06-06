using System;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class BaseWidgetViewModel
    {
        public class Details : UniqueEntity
        {
            public int Position { get; set; }
            public Guid LandingPageID { get; set; }
            public virtual LandingPageViewModel.Summary LandingPage { get; set; }
        }
    }

}