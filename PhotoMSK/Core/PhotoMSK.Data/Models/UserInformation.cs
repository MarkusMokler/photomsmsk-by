using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.ShoppingCart;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoMSK.Data.Models.Clients;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class UserInformation
    {
        public UserInformation()
        {
            Events = new Collection<Event>();
            Penalties = new List<Penalty>();
            CreationTime = DateTime.Now;
        }

        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPhoto { get; set; }
        public string ClientType { get; set; }
        public string Email { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime CreationTime { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserPhone> Phones { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<SaleCard> Cards { get; set; }
        public virtual ICollection<ShippingAddress> Adresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual List<RouteReview> RouteReview { get; set; } 
        public bool IsCup { get; set; }
        public bool Agreement { get; set; }
        public string VkLink { get; set; }
        public string FacebookLink { get; set; }
        public string Instagram { get; set; }

        public string Googleplus { get; set; }
        public string Site { get; set; }
        public virtual ICollection<RouteClientCategory> Categories { get; set; }

        public virtual bool IsInRole(Guid route, AccessStatus level)
        {
            return Roles.Any(x => x.Route.ID == route && x.AccessStatus <= level);
        }

        public class UserInformationConfiguration : EntityTypeConfiguration<UserInformation>
        {
            public UserInformationConfiguration()
            {
                HasKey(x => x.ID);

                HasOptional(x => x.User).WithOptionalDependent(x => x.UserInformation);

                HasMany(x => x.Penalties).WithRequired(x => x.User).HasForeignKey(x => x.UserInformationID);
                HasMany(x => x.Events).WithRequired(x => x.User).HasForeignKey(x => x.UserInformationID);
            }
        }
    }
}