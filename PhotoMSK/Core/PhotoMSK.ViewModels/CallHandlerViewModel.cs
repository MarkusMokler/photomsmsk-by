using System.Collections.Generic;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels
{
    public class CallHandlerViewModel
    {
        public UserInformationViewModel.Details UserInformation { get; set; }
        public List<PhotostudioViewModel.Detaills> Photostudios { get; set; }
        public List<PhotorentViewModel.Details> Photorents { get; set; }
        public string Phone { get; set; }
    }
}