using AutoMapper;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.ViewModels.Attachments
{
    public static class VideoViewModel
    {
        public class Summary : AttachmentViewModel.Summary
        {
            public string Type => "video";

        }

        public class Details : AttachmentViewModel.Details
        {
            public string Type => "video";
        }
    }

}
