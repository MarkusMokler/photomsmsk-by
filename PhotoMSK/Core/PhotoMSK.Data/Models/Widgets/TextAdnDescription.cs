using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Widgets
{
    public class TextAdnDescriptionWidget : BaseWidget
    {
        public Guid? PhotoID { get; set; }
        public virtual Photo Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class TextAdnDescriptionWidgetConfiguration : EntityTypeConfiguration<TextAdnDescriptionWidget>
    {
        public TextAdnDescriptionWidgetConfiguration()
        {
            ToTable("TextAdnDescriptionWidget");
        }
    }
}
