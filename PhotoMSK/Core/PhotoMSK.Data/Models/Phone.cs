using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PhotoMSK.Data.Models
{
    public class Phone
    {
        public virtual Guid ID { get; set; }
        public virtual UserPhone UserPhone { get; set; }
        public virtual ICollection<RoutesPhone> RoutesPhone { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Number { get; set; }
        public DateTime DateLastSending { get; set; }
    }
}