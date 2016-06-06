using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.ShoppingCart
{
    public class Order
    {
        public Guid ID { get; set; }
        public Guid StatusID { get; set; }
        public Guid UserInformationID { get; set; }
        public Guid? ShippingAdressID { get; set; }

        public DateTime OrderDate { get; set; }
        public double Total { get; set; }
        public double ShippingCost { get; set; }

        public virtual ShippingAddress ShippingAddress { get; set; }
        public virtual UserInformation UserInformation { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual Status Status { get; set; }
        public virtual Contract Contract { get; set; }

        public void Add(Event newevent)
        {
            OrderLines = OrderLines ?? new List<OrderLine>();
            OrderLines.Add(new EventOrderLine(newevent) { ID = Guid.NewGuid() });
            Total += newevent.Price;
        }
    }

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.UserInformation).WithMany(x => x.Orders).HasForeignKey(x => x.UserInformationID);
            HasOptional(x => x.Contract);
        }
    }
}
