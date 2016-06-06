using System;

namespace PhotoMSK.Data.Models.Attachments
{
    public abstract class Attachment : UniqueEntity
    {
        protected Attachment()
        {
            UploadDate = DateTime.Now;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
        public virtual WallPost Post { get; set; }
        public Guid? WallPostID { get; set; }
    }
}