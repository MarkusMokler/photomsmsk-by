using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class DescriptionWidget : BaseWidget
    {
        public string Content { get; set; }
    }



    public class DescriptionWidgetConfiguration : EntityTypeConfiguration<DescriptionWidget>
    {
        public DescriptionWidgetConfiguration()
        {
            ToTable("DescriptionWidget");
        }
    }
}
