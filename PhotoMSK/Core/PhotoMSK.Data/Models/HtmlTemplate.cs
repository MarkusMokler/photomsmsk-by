using System;

namespace PhotoMSK.Data.Models
{
    public class HtmlTemplate
    {
        public Guid ID { get; set; }
        public string ThemeName { get; set; }
        public string ThemeType { get; set; }
        public string ViewName { get; set; }
        public string Content { get; set; }

        public Guid RouteID { get; set; }
        public virtual RouteEntity Route { get; set; }
    }
}