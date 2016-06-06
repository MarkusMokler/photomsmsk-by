using System;
using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels
{
    public static partial class AdresseViewModel
    {
        public class Details
        {
            public virtual Guid ID { get; set; }
            public virtual UserViewModel.Summary From { get; set; }
            public virtual UserViewModel.Summary To { get; set; }
            public virtual MessageViewModel.Summary Message { get; set; }
            public bool Readed { get; set; }
            public string FromID { get; set; }
            public string ToID { get; set; }
        }

    }
}
