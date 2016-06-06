using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class RoutesPhone : UniqueEntity
    {
        public virtual Guid EntityID { get; set; }
        public virtual RouteEntity Entity { get; set; }
        public virtual Guid PhoneID { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual bool Confirm { get; set; }
        public virtual int ConfirmCode { get; set; }
        public virtual DateTime DateAdded { get; set; }


        public class RoutesPhoneConfiguration : EntityTypeConfiguration<RoutesPhone>
        {
            public RoutesPhoneConfiguration()
            {
                HasKey(x => x.ID);

                HasRequired(x => x.Entity).WithMany(x => x.Phones).HasForeignKey(x => x.EntityID);
                HasRequired(x => x.Phone).WithMany(x => x.RoutesPhone).HasForeignKey(x => x.PhoneID);
            }
        }
    }
}