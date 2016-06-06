using System;
using System.Collections.Generic;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class Genre
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Photomodel> Photomodels { get; set; }
    }
}