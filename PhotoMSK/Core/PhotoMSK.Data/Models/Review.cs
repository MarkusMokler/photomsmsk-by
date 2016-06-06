using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class Review : UniqueEntity
    {
        public virtual User User { get; set; }
        public virtual string User_Id { get; set; }
        public bool Like { get; set; }
    }

    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.User_Id);
        }
    }
}