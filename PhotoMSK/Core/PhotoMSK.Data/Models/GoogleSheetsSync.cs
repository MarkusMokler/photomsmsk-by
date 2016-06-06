using System;

namespace PhotoMSK.Data.Models
{
    public class GoogleSheetsSync
    {
        public Guid ID { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}