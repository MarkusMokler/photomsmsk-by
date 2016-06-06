using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class UserPhone
    {
        public virtual Guid ID { get; set; }
        public virtual Guid UserID { get; set; }
        public virtual UserInformation User { get; set; }
        //   public virtual Guid PhoneID { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual bool Confirm { get; set; }
        public virtual int ConfirmCode { get; set; }
        public virtual DateTime DateAdded { get; set; }

        public class UserPhoneConfiguration : EntityTypeConfiguration<UserPhone>
        {
            public UserPhoneConfiguration()
            {
                HasKey(x => x.ID);

                HasRequired(x => x.User).WithMany(x => x.Phones).HasForeignKey(x => x.UserID);
                HasRequired(x => x.Phone).WithRequiredDependent(x => x.UserPhone);
            }
        }
    }
}