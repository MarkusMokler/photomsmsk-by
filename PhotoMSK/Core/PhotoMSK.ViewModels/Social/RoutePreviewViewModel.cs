using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels
{
    public class RoutePreviewViewModel : UniqueEntity
    {
        public string Shortcut { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
    }
}