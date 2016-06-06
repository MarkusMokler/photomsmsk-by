using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models
{
    public class Message
    {
        public virtual Guid ID { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Adresse> Adresses { get; set; }
    }
}