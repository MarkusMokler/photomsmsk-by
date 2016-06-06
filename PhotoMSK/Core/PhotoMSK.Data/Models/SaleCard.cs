using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class SaleCard
    {
        public Guid ID { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public string CardType { get; set; }
        
        #region Pasport Data
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string PasportNumber { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string DepartmentOfIssue { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        #endregion

        public Guid UserInformationID { get; set; }
        public virtual UserInformation UserInformation { get; set; }

    }

    public class SaleCardConfigurator : EntityTypeConfiguration<SaleCard>
    {
        public SaleCardConfigurator()
        {
            HasKey(x => x.ID);

            HasRequired(x => x.UserInformation)
                .WithMany(photoshop => photoshop.Cards)
                .HasForeignKey(z => z.UserInformationID);
        }
    }
}