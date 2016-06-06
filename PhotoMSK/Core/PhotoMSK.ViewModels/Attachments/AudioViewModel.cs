using AutoMapper;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.ViewModels.Attachments
{
    public static class AudioViewModel
    {
        public class Summary : AttachmentViewModel.Summary
        {
            public string FileName { get; set; }
        }

        public class Details : AttachmentViewModel.Details
        {
            public string FileName { get; set; }
        }
    }
}
