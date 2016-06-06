using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Attachments
{
    public abstract class AttachmentViewModel
    {
        public abstract class Summary : UniqueEntity
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public DateTime UploadDate { get; set; }
            public Guid? WallPostID { get; set; }
        }

        public abstract class Details : Summary
        {
            public virtual WallPostViewModel.Summary Post { get; set; }
        }
    }

    
}
