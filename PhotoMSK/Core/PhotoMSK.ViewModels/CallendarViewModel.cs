using System;
using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels
{
    public class CalendarViewModel : UniqueEntity
    {
        public Guid CalendarID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public bool CanBook { get; set; }
        public string RouteName { get; set; }
    }
}