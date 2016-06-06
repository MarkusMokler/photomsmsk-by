using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class HallWidget : BaseWidget
    {
        public Guid HallID  { get; set; }
        public virtual Hall Hall { get; set; }
    }

    public class HallWidgetConfiguration : EntityTypeConfiguration<HallWidget>
    {
        public HallWidgetConfiguration()
        {
            ToTable("HallWidget");
        }
    }
}
