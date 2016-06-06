using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.ActivityStream;

namespace PhotoMSK.Data.Models.ActivityStream
{
    public class CallActivity : Activity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public CallType CallType { get; set; }
        public string VoiceRecord { get; set; }
        public Guid? EventID { get; set; }
        public Event Event { get; set; }

    }

    public class CallActivityConfiguration : EntityTypeConfiguration<CallActivity>
    {
        public CallActivityConfiguration()
        {
            ToTable("CallActivity");
        }
    }

}