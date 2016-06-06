using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class SplitWidget : BaseWidget
    {
        public Guid LeftID{get; set;}
        public BaseWidget Left { get; set; }
        public Guid RightID { get; set; }
        public BaseWidget Right { get; set; }
    }



    public class SplitWidgetConfiguration : EntityTypeConfiguration<SplitWidget>
    {
        public SplitWidgetConfiguration()
        {
            ToTable("SplitWidget");
        }
    }
}
