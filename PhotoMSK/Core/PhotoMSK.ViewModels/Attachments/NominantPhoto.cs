using System;
using PhotoMSK.ViewModels.Routes;

namespace PhotoMSK.ViewModels.Attachments
{
    public class NominantPhotoViewModel
    {
        public class Summary
        {
            public PhotoViewModel.Summary Photo { get; set; }
            public PhotographerViewModel.Summary Photographer { get; set; }
            public Guid MonthId { get; set; }
            public string Place { get; set; }
        }
    }
}
