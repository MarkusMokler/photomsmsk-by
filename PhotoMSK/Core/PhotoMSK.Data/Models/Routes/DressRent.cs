using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Routes
{
    public class DressRent : RouteEntity
    {
        public string Name { get; set; }
        public override string GetName() => Name;
        public virtual ICollection<Dress> Dresses { get; set; }
    }

    public class DressRentConfiguration : EntityTypeConfiguration<DressRent>
    {
        public DressRentConfiguration()
        {
            ToTable("DressRent");
        }
    }

}
