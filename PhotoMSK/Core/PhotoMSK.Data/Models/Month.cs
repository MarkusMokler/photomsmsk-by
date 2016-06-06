using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMSK.Data.Models
{
    public class Month : UniqueEntity
    {
        public DateTime DateMonth { get; set; }
        public virtual ICollection<NominationPhoto> NominationPhotos { get; set; }
    }
}