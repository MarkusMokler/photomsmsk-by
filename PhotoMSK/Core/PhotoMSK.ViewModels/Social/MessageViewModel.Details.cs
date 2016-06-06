using System;
using System.Collections.Generic;

namespace PhotoMSK.ViewModels
{
    public static partial class MessageViewModel
    {
        public class Details
        {
            public virtual Guid ID { get; set; }
            public DateTime Time { get; set; }
            public string Text { get; set; }
            public virtual ICollection<AdresseViewModel.Summary> Adresses { get; set; }
        }
    }
}
