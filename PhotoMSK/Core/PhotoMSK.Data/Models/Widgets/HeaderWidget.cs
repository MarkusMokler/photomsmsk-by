using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class HeaderWidget : BaseWidget
    {
        public string Content { get; set; }
    }



    public class HeaderWidgetConfiguration : EntityTypeConfiguration<HeaderWidget>
    {
        public HeaderWidgetConfiguration()
        {
            ToTable("HeaderWidget");
        }
    }
}
