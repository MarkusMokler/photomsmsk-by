using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Widgets
{
    public class CalendarsWidget : BaseWidget
    {
        public virtual ICollection<CalendarWidget> CalendarWidgets { get; set; }
    }

    public class CalendarsWidgetConfiguration : EntityTypeConfiguration<CalendarsWidget>
    {
        public CalendarsWidgetConfiguration()
        {
            ToTable("CalendarsWidget");
        }
    }

    public class CalendarWidget : SortedEntry
    {
        public Guid WidgetID { get; set; }
        public CalendarsWidget Widget { get; set; }

        public Guid CalendarID { get; set; }
        public Calendar Calendar { get; set; }
    }
}
