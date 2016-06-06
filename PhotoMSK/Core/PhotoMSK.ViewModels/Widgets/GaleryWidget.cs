using System.Collections.Generic;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Attachments;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class GalleryWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "gallery";
            public virtual List<FileEntryViewModel.Details> Files { get; set; }
        }
    }
}