using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.ShoppingCart
{
    public class StatusID
    {
        public static Guid NewOrder = Guid.Parse("cae1dc93-605b-46bc-8cf5-11bcfebd1523");

    }

    public class Status
    {
        public Guid ID { get; set; }
        public virtual List<Order> Orders { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class StatusConfiguration : EntityTypeConfiguration<Status>
    {
        public StatusConfiguration()
        {
            HasKey(x => x.ID);
            HasMany(x => x.Orders).WithRequired(x => x.Status).HasForeignKey(x => x.StatusID);
        }
    }
}
