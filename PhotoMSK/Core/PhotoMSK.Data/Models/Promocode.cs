using System;

namespace PhotoMSK.Data.Models
{
    public class Promocode
    {
        public Guid ID { get; set; }
        public Guid Code { get; set; }
        public bool Active { get; set; }
        public double Percentage { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public RouteEntity OwnerEntity { get; set; }
    }
}