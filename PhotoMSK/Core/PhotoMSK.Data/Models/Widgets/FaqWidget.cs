using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class FaqWidget : BaseWidget
    {
        public virtual List<QuestionAnswer> QuestionAnswers { get; set; }
    }


    public class FaqWidgetWidgetConfiguration : EntityTypeConfiguration<FaqWidget>
    {
        public FaqWidgetWidgetConfiguration()
        {
            ToTable("FaqWidget");
        }
    }
}
