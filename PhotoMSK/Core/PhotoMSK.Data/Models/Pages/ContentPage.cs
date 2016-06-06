using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.ActivityStream;

namespace PhotoMSK.Data.Models.Pages
{
    public class ContentPage : BasePage
    {
        public string Content { get; set; }
    }
    public class ContentPageConfiguration : EntityTypeConfiguration<ContentPage>
    {
        public ContentPageConfiguration()
        {
            ToTable("ContentPage");
        }
    }
}
