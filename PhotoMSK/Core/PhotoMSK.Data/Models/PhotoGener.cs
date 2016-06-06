using System;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class PhotoGener
    {
        public Guid ID { get; set; }
        public virtual Photographer Photographer { get; set; }
        public virtual Genre Genre { get; set; }
        public int Level { get; set; }
    }
}