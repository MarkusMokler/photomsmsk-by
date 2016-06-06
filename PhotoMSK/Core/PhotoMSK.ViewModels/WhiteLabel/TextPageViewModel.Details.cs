using PhotoMSK.ViewModels.RoutePageLayouts;

namespace PhotoMSK.ViewModels
{
    public static partial class TextPageViewModel
    {
        public class Details : TextPageViewModel.Summary
        {
            public RoutePageLayoutsViewModel PageLayout { get; set; }
        }
    }
}