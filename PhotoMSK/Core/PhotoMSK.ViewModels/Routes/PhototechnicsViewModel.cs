using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public static class PhototechnicsViewModel
    {
        public class Details : RouteEntityViewModel.Details
        {
            public string CategorySlug { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public string ShortDescription { get; set; }

            public decimal Price { get; set; }
            public double HourlyPrice { get; set; }
            public double HalfDayPrice { get; set; }
            public double DaylyPrice { get; set; }
            public double HollidayPrice { get; set; }
            public double WeeklyPrice { get; set; }
            public double MonthlyPrice { get; set; }

            public decimal CalculatedPrice { get; set; }
            public double CalculatedHourlyPrice { get; set; }
            public double CalculatedHalfDayPrice { get; set; }
            public double CalculatedDaylyPrice { get; set; }
            public double CalculatedHollidayPrice { get; set; }
            public double CalculatedWeeklyPrice { get; set; }
            public double CalculatedMonthlyPrice { get; set; }

            public virtual ICollection<PricePositionViewModel> Prices { get; set; }

            public List<RentViewModel> Rents { get; set; }
            public string RouteShortcut { get; set; }
            public IList<ParameterViewModel> Parameters { get; set; }
            public string Warranty { get; set; }
            public override string RouteType => "Phototechnics";

        }

        public class Summary : RouteEntityViewModel.Summary
        {
            public Guid PhototechnicsID { get; set; }
            public Guid PhotorentID { get; set; }
            public string CategorySlug { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public string ShortDescription { get; set; }

            public decimal Price { get; set; }
            public double HourlyPrice { get; set; }
            public double HalfDayPrice { get; set; }
            public double DaylyPrice { get; set; }
            public double HollidayPrice { get; set; }
            public double WeeklyPrice { get; set; }
            public double MonthlyPrice { get; set; }

            public decimal CalculatedPrice { get; set; }
            public double CalculatedHourlyPrice { get; set; }
            public double CalculatedHalfDayPrice { get; set; }
            public double CalculatedDaylyPrice { get; set; }
            public double CalculatedHollidayPrice { get; set; }
            public double CalculatedWeeklyPrice { get; set; }
            public double CalculatedMonthlyPrice { get; set; }

            public List<RentViewMode> Rents { get; set; }
            public string RouteShortcut { get; set; }
            public override string RouteType => "Phototechnics";
        }
    }
    public class RentViewMode
    {
        public string Name { get; set; }
        public Guid CalendarID { get; set; }
    }

    public class RentViewModel
    {
        public string Name { get; set; }
        public Guid CalendarID { get; set; }
        public double DaylyPrice { get; set; }
        public virtual PhotorentViewModel.Summary Photorent { get; set; }
    }

    public class PhototechnicsPrifile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<RentCalendar, PhototechnicsViewModel.Summary>()
         .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
         .ForMember(dest => dest.PhototechnicsID, source => source.MapFrom(mem => mem.PhototechnicsID))
         .ForMember(dest => dest.PhotorentID, source => source.MapFrom(mem => mem.PhotorentID))
         .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Phototechnics.Shortcut))
         .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Phototechnics.Name))
         .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.Phototechnics.ImageUrl))
         .ForMember(dest => dest.ShortDescription, source => source.MapFrom(mem => mem.Phototechnics.Description))
         .ForMember(dest => dest.CategorySlug, source => source.MapFrom(mem => mem.Phototechnics.Category.Slug))
         .ForMember(dest => dest.RouteShortcut, source => source.MapFrom(mem => mem.Photorent.Shortcut))
         .ForMember(dest => dest.Rents,
             source =>
                 source.MapFrom(
                     mem =>
                         new List<RentViewMode>
                                {
                                    new RentViewMode {Name = mem.GetRouteName(), CalendarID = mem.ID}
                                }))
         .ForMember(dest => dest.CalculatedHourlyPrice, source => source.MapFrom(mem => mem.HourlyPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.CalculatedHalfDayPrice, source => source.MapFrom(mem => mem.HalfDayPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.CalculatedDaylyPrice, source => source.MapFrom(mem => mem.DaylyPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.CalculatedHollidayPrice, source => source.MapFrom(mem => mem.HollidayPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.CalculatedWeeklyPrice, source => source.MapFrom(mem => mem.WeeklyPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.CalculatedMonthlyPrice, source => source.MapFrom(mem => mem.MonthlyPrice * mem.Photorent.CurrencyValue))
         .ForMember(dest => dest.HourlyPrice, source => source.MapFrom(mem => mem.HourlyPrice))
         .ForMember(dest => dest.HalfDayPrice, source => source.MapFrom(mem => mem.HalfDayPrice))
         .ForMember(dest => dest.DaylyPrice, source => source.MapFrom(mem => mem.DaylyPrice))
         .ForMember(dest => dest.HollidayPrice, source => source.MapFrom(mem => mem.HollidayPrice))
         .ForMember(dest => dest.WeeklyPrice, source => source.MapFrom(mem => mem.WeeklyPrice))
         .ForMember(dest => dest.MonthlyPrice, source => source.MapFrom(mem => mem.MonthlyPrice));

            CreateMap<Data.Models.Routes.Phototechnics, PhototechnicsViewModel.Summary>()
                .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
                .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Shortcut))
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.ImageUrl))
                .ForMember(dest => dest.Rents, source => source.MapFrom(mem => mem.Rents))
                .ForMember(dest => dest.Price, source => source.ResolveUsing<PriceResolver>().FromMember(opt => opt.Prices))

                .ForMember(dest => dest.CalculatedHourlyPrice, source => source.ResolveUsing<CalculatedHourlyPriceResolver>().FromMember(opt => opt.Rents))
                .ForMember(dest => dest.CalculatedHalfDayPrice, source => source.ResolveUsing<CalculatedHalfDayPriceResolver>().FromMember(opt => opt.Rents))
                .ForMember(dest => dest.CalculatedDaylyPrice, source => source.ResolveUsing<CalculatedDaylyPriceResolver>().FromMember(opt => opt.Rents))
                .ForMember(dest => dest.CalculatedHollidayPrice, source => source.ResolveUsing<CalculatedHollydayPriceResolver>().FromMember(opt => opt.Rents))
                .ForMember(dest => dest.CalculatedWeeklyPrice, source => source.ResolveUsing<CalculatedWeeklyPriceResolver>().FromMember(opt => opt.Rents))
                .ForMember(dest => dest.CalculatedMonthlyPrice, source => source.ResolveUsing<CalculatedMonthlyPriceResolver>().FromMember(opt => opt.Rents))

                .ForMember(dest => dest.CalculatedPrice, source => source.ResolveUsing<CalculatedPriceResolver>().FromMember(opt => opt.Prices));

            CreateMap<PricePosition, PhototechnicsViewModel.Summary>()
              .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.Phototechnics.ID))
              .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Phototechnics.Shortcut))
              .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Phototechnics.Name))
              .ForMember(dest => dest.CategorySlug, source => source.MapFrom(mem => mem.Phototechnics.Category.Slug))
              .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.Phototechnics.ImageUrl))
              .ForMember(dest => dest.ShortDescription, source => source.MapFrom(mem => mem.Phototechnics.Description))
              .ForMember(dest => dest.RouteShortcut, source => source.MapFrom(mem => mem.Photoshop.Shortcut))
              .ForMember(dest => dest.CalculatedPrice, source => source.MapFrom(mem => mem.Price * mem.Photoshop.CurencyValue));

            CreateMap<RentCalendar, RentViewMode>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.GetRouteName()))
                .ForMember(dest => dest.CalendarID, source => source.MapFrom(mem => mem.ID));

            CreateMap<RentCalendar, PhototechnicsViewModel.Details>()
                .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.Phototechnics.ID))
                .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Phototechnics.Shortcut))
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Phototechnics.Name))
                 .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Phototechnics.Description))
                .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.Phototechnics.ImageUrl))
                .ForMember(dest => dest.ShortDescription, source => source.MapFrom(mem => mem.Phototechnics.Description))
                .ForMember(dest => dest.CategorySlug, source => source.MapFrom(mem => mem.Phototechnics.Category.Slug))
                .ForMember(dest => dest.RouteShortcut, source => source.MapFrom(mem => mem.Photorent.Shortcut))
                .ForMember(dest => dest.Rents,
                    source =>
                        source.MapFrom(
                            mem =>
                                new List<RentViewModel>
                                {
                                    new RentViewModel
                                    {
                                        Name = mem.GetRouteName(),
                                        CalendarID = mem.ID,
                                        DaylyPrice = mem.DaylyPrice * mem.Photorent.CurrencyValue
                                    }
                                }))
                .ForMember(dest => dest.CalculatedHourlyPrice, source => source.MapFrom(mem => mem.HourlyPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.CalculatedHalfDayPrice, source => source.MapFrom(mem => mem.HalfDayPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.CalculatedDaylyPrice, source => source.MapFrom(mem => mem.DaylyPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.CalculatedHollidayPrice, source => source.MapFrom(mem => mem.HollidayPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.CalculatedWeeklyPrice, source => source.MapFrom(mem => mem.WeeklyPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.CalculatedMonthlyPrice, source => source.MapFrom(mem => mem.MonthlyPrice * mem.Photorent.CurrencyValue))
                .ForMember(dest => dest.HourlyPrice, source => source.MapFrom(mem => mem.HourlyPrice))
                .ForMember(dest => dest.HalfDayPrice, source => source.MapFrom(mem => mem.HalfDayPrice))
                .ForMember(dest => dest.DaylyPrice, source => source.MapFrom(mem => mem.DaylyPrice))
                .ForMember(dest => dest.HollidayPrice, source => source.MapFrom(mem => mem.HollidayPrice))
                .ForMember(dest => dest.WeeklyPrice, source => source.MapFrom(mem => mem.WeeklyPrice))
                .ForMember(dest => dest.MonthlyPrice, source => source.MapFrom(mem => mem.MonthlyPrice));

            CreateMap<Data.Models.Routes.Phototechnics, PhototechnicsViewModel.Details>()
                .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.ID))
                .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Shortcut))
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Name))
                .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.ImageUrl))
                .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Description))
                .ForMember(dest => dest.Rents, source => source.MapFrom(mem => mem.Rents))
                .ForMember(dest => dest.Price, source => source.MapFrom(mem => mem.Prices.Min(x => x.Price)));

            CreateMap<PricePosition, PhototechnicsViewModel.Details>()
                .ForMember(dest => dest.ID, source => source.MapFrom(mem => mem.Phototechnics.ID))
              .ForMember(dest => dest.Shortcut, source => source.MapFrom(mem => mem.Phototechnics.Shortcut))
              .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.Phototechnics.Name))
              .ForMember(dest => dest.CategorySlug, source => source.MapFrom(mem => mem.Phototechnics.Category.Slug))
              .ForMember(dest => dest.Image, source => source.MapFrom(mem => mem.Phototechnics.ImageUrl))
              .ForMember(dest => dest.Description, source => source.MapFrom(mem => mem.Phototechnics.Description))
              .ForMember(dest => dest.RouteShortcut, source => source.MapFrom(mem => mem.Photoshop.Shortcut))
               .ForMember(dest => dest.Parameters, source => source.MapFrom(mem => mem.Phototechnics.ParameterValues))
                     .ForMember(dest => dest.Warranty, source => source.MapFrom(mem => mem.Warranty))
              .ForMember(dest => dest.CalculatedPrice, source => source.MapFrom(mem => mem.Price * mem.Photoshop.CurencyValue));

            CreateMap<RentCalendar, RentViewModel>()
                .ForMember(dest => dest.Name, source => source.MapFrom(mem => mem.GetRouteName()))
                .ForMember(dest => dest.CalendarID, source => source.MapFrom(mem => mem.ID))
                .ForMember(dest => dest.Photorent, source => source.MapFrom(mem => mem.Photorent));
        }

        public class PriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<PricePosition>;

                return pp != null ? source.New(!pp.Any() ? 0 : pp.Min(x => x.Price)) : source.New(0);
            }

        }

        public class CalculatedPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<PricePosition>;


                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.Price * x.Photoshop.CurencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }


        public class CalculatedHourlyPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.HourlyPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }

        public class CalculatedHalfDayPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.HalfDayPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }


        public class CalculatedDaylyPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.DaylyPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }

        public class CalculatedHollydayPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.HollidayPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }

        public class CalculatedWeeklyPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.WeeklyPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }

        public class CalculatedMonthlyPriceResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {

                var pp = source.Value as IEnumerable<RentCalendar>;

                if (pp == null) return source.New(0);
                var items = pp.Select(x => x.MonthlyPrice * x.Photorent.CurrencyValue).Where(x => x > 0).ToList();
                return source.New(items.Any() ? items.Min() : 0);
            }

        }
    }
}
public static class PhototechnicsDefinition
{
    public static string Shortcut { get { return "<%- shortcut %>"; } }
    public static string Name { get { return "<%- name %>"; } }
    public static string Image { get { return "<%- image %>"; } }
    public static string CalculatedPrice { get { return "<%- App.Tools.formatMoney(calculatedPrice) %>"; } }
    public static string ShortDescription { get { return "<%= App.Tools.htmlSubstring(shortDescription,120) %>"; } }

    public static string CalculatedHourlyPrice { get { return "<%- App.Tools.formatMoney(calculatedHourlyPrice) %>"; } }
    public static string CalculatedHalfDayPrice { get { return "<%- App.Tools.formatMoney(calculatedHalfDayPrice) %>"; } }
    public static string CalculatedDaylyPrice { get { return "<%- App.Tools.formatMoney(calculatedDaylyPrice) %>"; } }
    public static string CalculatedHollidayPrice { get { return "<%- App.Tools.formatMoney(calculatedHollidayPrice) %>"; } }
    public static string CalculatedWeeklyPrice { get { return "<%- App.Tools.formatMoney(calculatedWeeklyPrice) %>"; } }
    public static string CalculatedMonthlyPrice { get { return "<%- App.Tools.formatMoney(calculatedMonthlyPrice) %>"; } }
    public static string Url { get { return "<%= url %>"; } }
}
