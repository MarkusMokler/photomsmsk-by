using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Routes
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<CategoryBrand> Brands { get; set; }
        public virtual ICollection<Phototechnics> Phototechnics { get; set; }
    }
}