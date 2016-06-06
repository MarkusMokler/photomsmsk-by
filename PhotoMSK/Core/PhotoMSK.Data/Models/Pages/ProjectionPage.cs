using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Pages
{
    public class ProjectionPage : BasePage
    {
        public Guid? ChildPageCategoryID { get; set; }
        public virtual PageCategory ChildPageCategory { get; set; }
    }

    public class ProjectionPageConfiguration : EntityTypeConfiguration<ProjectionPage>
    {
        public ProjectionPageConfiguration()
        {
            ToTable("ProjectionPage");
        }
    }
}
