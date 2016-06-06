using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Statistics
{
    public class WeekBalance : UniqueEntity
    {
        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }
        private readonly List<BalanceLine> _balanceLines = new List<BalanceLine>();
        public double StartBalance { get; set; }
        public double EndBalance { get; set; }
        public bool Closed { get; set; }

        public virtual ICollection<BalanceLine> BalanceLines { get; set; }

        public void AddBalance(BalanceLine line)
        {
            if (BalanceLines == null)
                BalanceLines = new List<BalanceLine>();
            BalanceLines.Add(line);
        }

        public int WeekOfMonth { get; set; }
        public int WeekOfYear { get; set; }
        public int Year { get; set; }
    }
}