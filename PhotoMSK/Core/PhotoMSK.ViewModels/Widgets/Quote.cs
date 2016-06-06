namespace PhotoMSK.ViewModels.Widgets
{
    public static class QuoteWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Text { get; set; }
            public string Autor { get; set; }
            public string AutorLink { get; set; }
        }
    }
}