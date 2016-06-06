using System;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models
{
    public class Adresse
    {
        public virtual Guid ID { get; set; }
        public virtual User From { get; set; }
        public virtual User To { get; set; }
        public virtual Message Message { get; set; }
        public bool Readed { get; set; }
        public string FromID { get; set; }
        public string ToID { get; set; }
    }

    public class AdresseConfigration : EntityTypeConfiguration<Adresse>
    {
        public AdresseConfigration()
        {
            HasRequired(x => x.From).WithMany(x => x.SendedMessages).HasForeignKey(x => x.FromID);
            HasRequired(x => x.To).WithMany(x => x.RecivedMessages).HasForeignKey(x => x.ToID);
            HasRequired(x => x.Message).WithMany(x => x.Adresses);
        }
    }
}