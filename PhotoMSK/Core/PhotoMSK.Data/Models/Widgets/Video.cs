using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Widgets
{
    public class VideoWidget : BaseWidget
    {
        public Guid? VideoID { get; set; }
        public virtual Video Video { get; set; }

    }


    public class VideoWidgetConfiguration : EntityTypeConfiguration<VideoWidget>
    {
        public VideoWidgetConfiguration()
        {
            ToTable("VideoWidget");

        }
    }

}
