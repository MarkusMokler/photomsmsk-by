namespace PhotoMSK.ViewModels.Interfaces
{
    public class RouteObject
    {
        public string Domain{ get; set; }
        public string RouteShortcut{ get; set; }
        public string CategorySlug{ get; set; }
        public string BrandSlug{ get; set; }
        public string Shortcut{ get; set; }
        public string Action{ get; set; }
        public string Controller{ get; set; }
        public string Id { get; set; }
        public bool WhiteLabel { get; set; }
    }
}