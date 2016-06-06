using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class CalendarsWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "calendarWidget";
            public virtual ICollection<CalendarWidgetViewModel.Details> CalendarWidgets { get; set; }
        }

    }

    public class CalendarWidgetViewModel
    {
        public class Details 
        {
            public Guid WidgetID { get; set; }
            public Guid ID { get; set; }
            public Guid CalendarID { get; set; }
        }
    }
}
