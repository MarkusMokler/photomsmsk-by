using System;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.ViewModels.Attachments
{

    public static class FileEntryViewModel
    {
        public class Summary : UniqueEntity
        {
            public Guid? ParentEntryID { get; set; }
            public string Name { get; set; }
            public FileEntryType Type { get; set; }
            public Guid? AttachmentID { get; set; }
        }

        public class Details : UniqueEntity
        {
            public Guid? ParentEntryID { get; set; }
            public string Name { get; set; }
            public virtual Summary ParentEntry { get; set; }
            public FileEntryType Type { get; set; }
            public Guid? AttachmentID { get; set; }
            public virtual AttachmentViewModel.Details Attachment { get; set; }
        }
    }

}
