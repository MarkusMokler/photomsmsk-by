using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class ShootingGenre
    {
        public Guid ID { get; set; }
        public Genre Genre { get; set; }
        public Shooting Shooting { get; set; }
        public Guid GenreID { get; set; }
        public Guid ShootingID { get; set; }
    }

    public class ShootingGenreConfiguration : EntityTypeConfiguration<ShootingGenre>
    {
        public ShootingGenreConfiguration()
        {
            HasRequired(x => x.Genre).WithMany().HasForeignKey(x => x.GenreID);
            HasRequired(x => x.Shooting).WithMany(x => x.Geners).HasForeignKey(x => x.ShootingID);
        }
    }
}