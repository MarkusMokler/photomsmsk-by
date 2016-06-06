using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoMSK.Models.EditViewModel
{
    public class MenuItemEditModel : AbstractEditModel
    {
        public Guid? ID { get; set; }
        public Guid? RouteID { get; set; }
        public String Name { get; set; }
        public String Order { get; set; }
        public bool? Publish { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public Guid? TextPageID { get; set; }

    }
}