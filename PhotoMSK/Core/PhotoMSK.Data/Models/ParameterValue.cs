using System;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class ParameterValue
    {
        public Guid ID { get; set; }
        public virtual Parameter Parameter { get; set; }
        public virtual Phototechnics Phototechnics { get; set; }
        public string Value { get; set; }
    }
}