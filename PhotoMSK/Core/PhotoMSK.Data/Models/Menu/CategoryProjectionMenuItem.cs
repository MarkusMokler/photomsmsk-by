using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Menu
{
    public class CategoryProjectionMenuItem : AbstractMenuItem
    {
        public Guid PageCategoryID { get; set; }
        public virtual PageCategory PageCategory { get; set; }
    }

    public class CategoryProjectionMenuItemConfiguration : EntityTypeConfiguration<CategoryProjectionMenuItem>
    {
        public CategoryProjectionMenuItemConfiguration()
        {
            ToTable("CategoryProjectionMenuItem");
        }
    }
}
