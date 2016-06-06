using System;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Widgets
{
    public class FileGallery : SortedEntry
    {
        public Guid FileEntryID { get; set; }
        public virtual FileEntry FileEntry { get; set; }
        public Guid GalleryWidgetID { get; set; }
        public GalleryWidget GalleryWidget { get; set; }
    }
}