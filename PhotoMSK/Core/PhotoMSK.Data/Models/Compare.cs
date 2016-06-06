using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMSK.Data.Models
{
    public class Compare : UniqueEntity
    {
        public Guid NominationPhoto1ID { get; set; }
        public virtual NominationPhoto NominationPhoto1 { get; set; }

        public Guid NominationPhoto2ID { get; set; }
        public virtual NominationPhoto NominationPhoto2 { get; set; }

        public Guid UserID { get; set; }
        public virtual UserInformation User { get; set; }
    }
}