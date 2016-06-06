using System;

namespace PhotoMSK.Data.Models
{
    public abstract class UniqueEntity
    {
        public Guid ID { get; set; }
    }

    public abstract class SortedEntry : UniqueEntity
    {
        public int Position { get; set; }
    }

    public abstract class NamedEntity : UniqueEntity
    {
        public string Name { get; set; }

    }

    public abstract class HistoricalEntity : UniqueEntity
    {
        public int Version { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; private set; }
        public Guid ModifiedByID { get; set; }
        public virtual UserInformation ModifiedBy { get; set; }

        public virtual void ApplyModification(Guid uid)
        {
            Version++;
            ModifiedTime = DateTime.Now;
            ModifiedByID = uid;
        }

    }

}