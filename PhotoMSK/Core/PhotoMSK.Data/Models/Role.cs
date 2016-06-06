using System;
using PhotoMSK.Data.Enums;

namespace PhotoMSK.Data.Models
{
    public class Role
    {
        public Guid ID { get; set; }
        public virtual UserInformation UserInformation { get; set; }
        public virtual RouteEntity Route { get; set; }
        public virtual AccessStatus AccessStatus { get; set; }
    }
}