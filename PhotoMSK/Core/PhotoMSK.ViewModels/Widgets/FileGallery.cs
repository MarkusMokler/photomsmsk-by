using System;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.ViewModels.Widgets
{
    public class FileGallery : SortedEntry
    {
        public Guid FileEntryID { get; set; }
        public FileEntry FileEntry { get; set; }
        public Guid GalleryWidgetID { get; set; }
        public GalleryWidget GalleryWidget { get; set; }
    }
}