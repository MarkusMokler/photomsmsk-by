using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar_importer
{
    public class ImportingEvent
    {
        public string ID { get; set; }
        public bool Sync { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Description { get; set; }
    }
}
