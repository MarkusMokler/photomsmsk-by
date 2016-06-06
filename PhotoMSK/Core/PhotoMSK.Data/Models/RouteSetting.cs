using System;

namespace PhotoMSK.Data.Models
{
    public class RouteSetting : UniqueEntity
    {
        public Guid SettingID { get; set; }
        public virtual Setting Setting { get; set; }
        public virtual Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }
        public string SettingValue { get; set; }
        public bool Ennabled { get; set; }
    }
}