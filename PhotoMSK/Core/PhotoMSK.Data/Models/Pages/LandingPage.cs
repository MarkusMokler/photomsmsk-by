using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Pages
{
    public class LandingPage : BasePage
    {

    }
    public class LandingPageConfiguration : EntityTypeConfiguration<LandingPage>
    {
        public LandingPageConfiguration()
        {
            ToTable("LandingPage");
        }
    }
}
