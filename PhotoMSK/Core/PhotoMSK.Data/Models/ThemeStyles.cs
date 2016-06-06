using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models
{
    public class ThemeStyles
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public string Name { get; set; }
        public virtual RouteEntity Route { get; set; }
        public string Style { get; set; }
    }

    public class ThemeStylesConfiguration : EntityTypeConfiguration<ThemeStyles>
    {
        public ThemeStylesConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Route).WithMany(x => x.Styles).HasForeignKey(x => x.RouteID);
        }
    }
}
