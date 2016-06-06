using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.ActivityStream;

namespace PhotoMSK.Data.Models.Widgets
{
    public class BaseWidget : UniqueEntity
    {
        public int Position { get; set; }
        public Guid ZoneID { get; set; }
        public virtual Zone Zone { get; set; }
    }


    public class BaseWidgetConfiguration : EntityTypeConfiguration<BaseWidget>
    {
        public BaseWidgetConfiguration()
        {
            ToTable("BaseWidget");
        }
    }

}
