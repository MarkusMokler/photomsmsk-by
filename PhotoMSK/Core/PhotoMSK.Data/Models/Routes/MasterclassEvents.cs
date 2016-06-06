using System;
using PhotoMSK.Data.Enums;

namespace PhotoMSK.Data.Models.Routes
{
    public class MasterclassEvents
    {
        public Guid ID { get; set; }
        public DateTime Time { get; set; }
        public MasterEventType EventType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}