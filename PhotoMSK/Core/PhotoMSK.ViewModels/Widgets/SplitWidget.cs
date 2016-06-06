using System;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class SplitWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type = "splitWidget";
            public Guid LeftID { get; set; }
            public BaseWidgetViewModel.Details Left { get; set; }
            public Guid RightID { get; set; }
            public BaseWidgetViewModel.Details Right { get; set; }
        }
    }
}