using System.IO;

namespace PhotoMSK.Data.Models
{
    public class SmsModule : UniqueEntity
    {
        public bool SmsEnnabled { get; set; }
        public string BookingLineText { get; set; }
        public double SmsBalance { get; set; }
        public string SmsSender { get; set; }
        public virtual RouteEntity RouteEntity { get; set; }
        public string BookingHelloText { get; set; }
        public string BookingEndText { get; set; }
    }
}