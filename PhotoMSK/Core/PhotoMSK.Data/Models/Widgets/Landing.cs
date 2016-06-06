using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models.Widgets
{
    public class Zone : UniqueEntity
    {
        private int _addWidgetPosition = 0;

        public string Name { get; set; }
        public virtual List<BaseWidget> Widgets { get; set; }
        public Guid? LayoutID { get; set; }
        public virtual RoutepageLayout Layout { get; set; }

        public virtual void AddWidget(BaseWidget w)
        {
            w.Position = _addWidgetPosition;
            _addWidgetPosition++;
            Widgets.Add(w);
        }
    }


    public class LandingPageConfiguration : EntityTypeConfiguration<Zone>
    {
        public LandingPageConfiguration()
        {
            ToTable("Zones");
        }
    }
}
