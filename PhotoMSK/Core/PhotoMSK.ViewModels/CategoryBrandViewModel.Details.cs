using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.ViewModels
{
    public static partial class CategoryBrandViewModel
    {
        public class Details
        {
            public Guid ID { get; set; }
            public virtual Category Category { get; set; }
            public virtual Brand Brand { get; set; }
            public Guid CategoryID { get; set; }
            public Guid BrandID { get; set; }
        }
    }
}
