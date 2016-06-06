using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Photostudio
{
    public class IndexViewData : BasePageViewData<PhotostudioViewModel.Summary>
    {
        public string City { get; set; }
        public bool IsCity { get { return !string.IsNullOrEmpty(City); } }
    }
}