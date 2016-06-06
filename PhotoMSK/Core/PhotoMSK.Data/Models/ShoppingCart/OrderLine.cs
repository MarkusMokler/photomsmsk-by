using System;
using System.Data.Entity.ModelConfiguration;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models.ShoppingCart
{
    public class OrderLine
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public virtual Order Order { get; set; }
    }

    public class PhototechnicsOrderLine : OrderLine
    {
        public Guid PricePositionID { get; set; }
        public int Count { get; set; }
        public virtual PricePosition PricePosition { get; set; }
    }

    public class EventOrderLine : OrderLine
    {
        public EventOrderLine() { }
        public EventOrderLine(Event newevent)
        {
            this.Event = newevent;
        }

        public Guid EventID { get; set; }
        public Event Event { get; set; }

    }

    public class OrderLineMapping : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineMapping()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.Order).WithMany(x => x.OrderLines).HasForeignKey(x => x.OrderID);
        }

    }
    public class PhototechnicsOrderLineMapping : EntityTypeConfiguration<PhototechnicsOrderLine>
    {
        public PhototechnicsOrderLineMapping()
        {
            HasRequired(x => x.PricePosition).WithMany().HasForeignKey(x => x.PricePositionID);
        }
    }

    public class EventOrderLineMapping : EntityTypeConfiguration<EventOrderLine>
    {
        public EventOrderLineMapping()
        {

            HasRequired(x => x.Event).WithMany().HasForeignKey(x => x.EventID);
        }

    }
}
