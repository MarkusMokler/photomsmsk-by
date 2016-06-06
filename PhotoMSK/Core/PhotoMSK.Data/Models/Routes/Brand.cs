using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Routes
{
    public class Brand
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryBrand> Categories { get; set; }
        public virtual ICollection<Phototechnics> Phototechnics { get; set; }
    }
}