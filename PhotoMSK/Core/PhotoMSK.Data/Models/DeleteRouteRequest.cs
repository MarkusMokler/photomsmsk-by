using System;

namespace PhotoMSK.Data.Models
{
    public class DeleteRouteRequest
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public virtual RouteEntity Route { get; set; }
        public Guid RouteID { get; set; }
        public string Reason { get; set; }
    }
}
