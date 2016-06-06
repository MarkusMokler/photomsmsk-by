using System;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Statistics;

namespace PhotoMSK.ViewModels.Statistic
{
    public class BalanceLineViewModel : UniqueEntity
    {
        public class Summary
        {
            public int Year { get; set; }
            public string Title { get; set; }
            public DateTime? Date { get; set; }
            public double Total { get; set; }
            public BalanceLineType BalanceLineType { get; set; }
            public int? WeekOfMonth { get; set; }
            public int? WeekOfYear { get; set; }
            public Guid RouteID { get; set; }
            public Guid? WeekBalanceID { get; set; }
            public void AddEvent(Data.Models.Event @event)
            {
                Total += @event.Price;
            }
        }
    }
}