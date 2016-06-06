using System;

namespace PhotoMSK.Data.Models.Advertising
{
    public class Ad
    {
        public Guid ID { get; set; }
        public String Title { get; set; }
        public string Subtitle { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Visits { get; set; }
        public string Link { get; set; }
        public virtual AdCompany Company { get; set; }
    }
}