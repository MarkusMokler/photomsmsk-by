namespace PhotoMSK.ViewModels.Widgets
{
    public static class HeaderWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "headerWidget";
            public string Content { get; set; }
        }
    }
}