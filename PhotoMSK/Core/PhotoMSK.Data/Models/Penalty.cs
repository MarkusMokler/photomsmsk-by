using System;

namespace PhotoMSK.Data.Models
{
    public class Penalty
    {
        public Guid ID { get; set; }
        public decimal Price { get; set; }
        public decimal Paragraph { get; set; }
        public string Description { get; set; }
        public virtual UserInformation User { get; set; }
        public virtual Event Event { get; set; }
        public Guid UserInformationID { get; set; }
    }
}