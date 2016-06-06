using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Menu
{
    public class HallsMenuItem : AbstractMenuItem
    {
        public Guid HallID { get; set; }
        public virtual Hall Hall { get; set; }
    }

    public class HallsMenuItemConfiguration : EntityTypeConfiguration<HallsMenuItem>
    {
        public HallsMenuItemConfiguration()
        {
            ToTable("HallsMenuItem");
        }
    }
}
