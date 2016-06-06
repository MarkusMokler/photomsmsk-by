using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class ContainerWidget : BaseWidget
    {
        public Guid WidgetID { get; set; }
        public virtual BaseWidget Widget { get; set; }
    }



    public class ContainerWidgetConfiguration : EntityTypeConfiguration<ContainerWidget>
    {
        public ContainerWidgetConfiguration()
        {
            ToTable("ContainerWidget");
        }
    }
}
