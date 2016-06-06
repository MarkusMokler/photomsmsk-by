using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.Data.Models
{
    public class RoutepageLayout : NamedEntity
    {
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
        public string Category { get; set; }

        public Guid RouteID { get; set; }
        public RouteEntity Route { get; set; }

        public Guid? StyleID { get; set; }
        public virtual ThemeStyles Style { get; set; }
    }
}