using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Routes
{
    public class Dress : UniqueEntity
    {
        public virtual DressCalendar Calendar { get; set; }
        public Guid DressRentID { get; set; }
        public virtual DressRent DressRent { get; set; }
        public string Name { get; set; }

        public double GetPrice(DateTime start)
        {
            throw new NotImplementedException();
        }
    }
    public class DressConfiguration : EntityTypeConfiguration<Dress>
    {
        public DressConfiguration()
        {
            ToTable("Dress");
            HasOptional(x => x.Calendar).WithOptionalPrincipal(x => x.Dress);

        }
    }
}