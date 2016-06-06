using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Advertising
{
    public class AdCompany
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
    }
}