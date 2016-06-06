using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class QuoteWidget : BaseWidget
    {
        public string Text { get; set; }
        public string Autor { get; set; }
        public string AutorLink { get; set; }
    }



    public class QuoteWidgetConfiguration : EntityTypeConfiguration<QuoteWidget>
    {
        public QuoteWidgetConfiguration()
        {
            ToTable("QuoteWidget");
        }
    }
}
