using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Widgets
{
    public class GalleryWidget : BaseWidget
    {
        private int _position = 0;
        public virtual List<FileGallery> Files { get; set; } = new List<FileGallery>();

        public void AddFile(FileEntry file)
        {
            Files.Add(new FileGallery()
            {
                ID = Guid.NewGuid(),
                FileEntry = file,
                Position = _position
            });
            _position++;
        }

    }


    public class GalleryWidgetConfiguration : EntityTypeConfiguration<GalleryWidget>
    {
        public GalleryWidgetConfiguration()
        {
            ToTable("GalleryWidget");
        }
    }
}
