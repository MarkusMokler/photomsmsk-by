using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Clients
{
    public class RouteClientCategory
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }

        public Guid UserID { get; set; }
        public virtual UserInformation User { get; set; }

        public Guid TagID { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
