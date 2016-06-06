using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class UserMenuItem
    {
        public Guid ID { get; set; }
        public int Index { get; set; }
        public string Icon { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public virtual User User { get; set; }

        public class UserMenuItemConfiguration : EntityTypeConfiguration<UserMenuItem>
        {
            public UserMenuItemConfiguration()
            {
                HasKey(x => x.ID);
                HasRequired(x => x.User).WithMany(x => x.UserMenuItems);
            }
        }
    }
}