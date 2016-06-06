using PhotoMSK.Data.Models.Routes;
using System;

namespace PhotoMSK.Data.Models
{
    public class Project : UniqueEntity
    {
        public string Name { get; set; }
        public Guid PhotographerID { get; set; }
        public virtual Photographer Photographer { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string LandingPageUrl { get; set; }
        public string Type { get; set; }
        public string TeaserImage { get; set; }
        public string CoverImage { get; set; }
    }
}