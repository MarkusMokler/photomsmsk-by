using System;

namespace PhotoMSK.Data.Models.Statistics
{
    public class BalanceLine: UniqueEntity
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public double Total { get; set; }
        public BalanceLineType BalanceLineType { get; set; }
        public int? WeekOfMonth { get; set; }
        public int? WeekOfYear { get; set; }

        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }

        public Guid? WeekBalanceID { get; set; }
        public virtual WeekBalance WeekBalance { get; set; }
        public void AddEvent(Event @event)
        {
            Total += @event.Price;
        }
    }
}