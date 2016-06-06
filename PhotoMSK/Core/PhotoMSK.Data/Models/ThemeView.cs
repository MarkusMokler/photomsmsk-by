using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models
{
    public class ThemeView
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string RazorContent { get; set; }
        public Guid ThemeID { get; set; }
        public Theme Theme { get; set; }
    }
}
