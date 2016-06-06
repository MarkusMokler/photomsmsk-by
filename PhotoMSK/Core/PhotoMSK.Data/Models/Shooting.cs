using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class Shooting : WallPost
    {
        public bool CanEdit
        {
            get { return DateTime.Now.AddDays(-7) < Date; }
        }

        public virtual List<ShootingGenre> Geners { get; set; }

        public void AddGenre(Genre genre)
        {
            if (Geners == null)
                Geners = new List<ShootingGenre>();
            Geners.Add(new ShootingGenre {ID = Guid.NewGuid(), Genre = genre});
        }

        public class ShootingConfiguration : EntityTypeConfiguration<Shooting>
        {
            public ShootingConfiguration()
            {
                ToTable("Shooting");
            }
        }
    }
}