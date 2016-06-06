using System;
using System.Collections.Generic;

namespace PhotoMSK.ViewModels
{
    public static partial class HallPropertyViewModel
    {
        public class Details
        {
            public Guid ID { get; set; }
            public virtual ICollection<HallViewModel.Summary> Halls { get; set; }
            public string Name { get; set; }
            public int Group { get; set; }
            public int Index { get; set; }
        }
    }
}
