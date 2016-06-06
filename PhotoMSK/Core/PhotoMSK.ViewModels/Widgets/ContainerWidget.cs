using System;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class ContainerWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "containerWidget";
            public Guid WidgetID { get; set; }
            public virtual BaseWidgetViewModel.Details Widget { get; set; }
        }
    }
}