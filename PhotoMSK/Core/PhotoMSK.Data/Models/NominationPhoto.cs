using System;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Data.Models
{
    public class NominationPhoto : UniqueEntity
    {
        public int CompareCount { get; set; }  // Количество сравнений данной фотографии
        public int Point { get; set; }  // Количество голосов

        public Guid PhotoID { get; set; }
        public virtual Photo Photo { get; set; }

        public Guid MonthID { get; set; }
        public virtual Month Month { get; set; }

        public Guid PhotographerID { get; set; }
        public virtual Photographer Photographer { get; set; }
    }
}