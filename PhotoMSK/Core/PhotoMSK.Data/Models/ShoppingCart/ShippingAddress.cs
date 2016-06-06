using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.ShoppingCart
{
    public class ShippingAddress
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Guid UserInformationID { get; set; }
   
        public virtual UserInformation UserInformation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class ShippingAddressConfiguration : EntityTypeConfiguration<ShippingAddress>
    {
        public ShippingAddressConfiguration()
        {
            HasKey(x => x.ID);
            HasRequired(x => x.UserInformation).WithMany(x => x.Adresses).HasForeignKey(x => x.UserInformationID);
            HasMany(x => x.Orders).WithOptional(x => x.ShippingAddress).HasForeignKey(x => x.ShippingAdressID);
        }
    }
}
