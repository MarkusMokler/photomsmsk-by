using System;

namespace PhotoMSK.Data.Models
{
    public class SmsMessage
    {
        public Guid ID { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}