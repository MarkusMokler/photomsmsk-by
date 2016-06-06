using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models
{
   public class HallProperty
    {
        public Guid ID { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int Index { get; set; }
    }
}
