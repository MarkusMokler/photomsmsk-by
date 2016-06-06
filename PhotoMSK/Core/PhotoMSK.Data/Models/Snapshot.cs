using System;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models
{
    public class Snapshot : UniqueEntity
    {
        public virtual Photo Photo { get; set; }
        public DateTime SnapshotYear { get; set; }
    }
}