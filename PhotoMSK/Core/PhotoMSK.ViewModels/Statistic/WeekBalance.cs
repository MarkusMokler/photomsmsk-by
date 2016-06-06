using System;
using System.Collections.Generic;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Statistics;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Statistic
{
    public static class WeekBalanceViewModel
    {
        public class Details : UniqueEntity
        {
            public Guid RouteID { get; set; }

            private readonly List<BalanceLineViewModel.Summary> _balanceLines = new List<BalanceLineViewModel.Summary>();
            public double StartBalance { get; set; }
            public double EndBalance { get; set; }
            public bool Closed { get; set; }

            public virtual IList<BalanceLineViewModel.Summary> BalanceLines { get; set; }

            public void AddBalance(BalanceLineViewModel.Summary line)
            {
                if (BalanceLines == null)
                    BalanceLines = new List<BalanceLineViewModel.Summary>();
                BalanceLines.Add(line);
            }

            public int WeekOfMonth { get; set; }
            public int WeekOfYear { get; set; }
            public int Year { get; set; }
        }
    }

    public class CalendarReferenceSummaryProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<WeekBalance, WeekBalanceViewModel.Details>();
            CreateMap<BalanceLine, BalanceLineViewModel.Summary>();
        }
    }
}