using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.Data.Models.Attachments
{

    public class FileEntry : UniqueEntity
    {
        public Guid? ParentEntryID { get; set; }
        public string Name { get; set; }
        public virtual FileEntry ParentEntry { get; set; }
        public FileEntryType Type { get; set; }
        public Guid? AttachmentID { get; set; }
        public virtual Attachment Attachment { get; set; }

    }

    public enum FileEntryType
    {
        Folder, File
    }

    public class FileEntryConfiguration : EntityTypeConfiguration<FileEntry>
    {
        public FileEntryConfiguration()
        {
            ToTable("FileEntry");
            HasOptional(x => x.ParentEntry);
            HasOptional(x => x.Attachment);
        }
    }
}
