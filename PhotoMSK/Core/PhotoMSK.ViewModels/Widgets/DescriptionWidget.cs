namespace PhotoMSK.ViewModels.Widgets
{
    public static class DescriptionWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type = "descriptionWidget";
            public string Content { get; set; }
        }
    }
}