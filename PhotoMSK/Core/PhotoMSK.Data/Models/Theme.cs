using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models
{
    public class Theme
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThemeScrenshot { get; set; }
        public string RouteType { get; set; }
        public bool IsPrivate { get; set; }
        public bool AllowUserEdit { get; set; }
        public virtual ICollection<ThemeView> Views { get; set; } 
    }
}
