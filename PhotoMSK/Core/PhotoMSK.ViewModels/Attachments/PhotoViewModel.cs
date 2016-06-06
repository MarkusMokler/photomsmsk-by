using AutoMapper;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.ViewModels.Attachments
{
    public static class PhotoViewModel
    {
        public class Summary : AttachmentViewModel.Summary
        {
            public string Type => "photo";
            public string FileName { get; set; }
        }

        public class Details : AttachmentViewModel.Details
        {
            public string Type => "photo";
            public string FileName { get; set; }
        }
    }

}
