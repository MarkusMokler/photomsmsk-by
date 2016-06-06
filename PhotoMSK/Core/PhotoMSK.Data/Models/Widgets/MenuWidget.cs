using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Menu;

namespace PhotoMSK.Data.Models.Widgets
{
    public class MenuWidget : BaseWidget
    {
        public virtual Guid MenuID { get; set; }
        public virtual RouteMenu Menu { get; set; }
    }

    public class MenuWidgetConfiguration : EntityTypeConfiguration<MenuWidget>
    {
        public MenuWidgetConfiguration()
        {
            ToTable("MenuWidget");
        }
    }

}
