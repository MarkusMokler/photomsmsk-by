using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Attachments
{
    public class Creative : UniqueEntity
    {
        public PhotoGener Genre { get; set; }
        public Photo Photo { get; set; }
        public virtual LikeStatus LikeStatus { get; set; }

        public class CreativeConfiguration : EntityTypeConfiguration<Creative>
        {
            public CreativeConfiguration()
            {
                HasRequired(x => x.Photo);
                HasRequired(x => x.Genre);
            }
        }
    }
}