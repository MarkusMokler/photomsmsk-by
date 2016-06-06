using System;

namespace PhotoMSK.ViewModels
{
    public static partial class CopyReferenceViewModel
    {
        public class Details
        {
            public Guid ID { get; set; }
            public int CopyFrom { get; set; }
            public int LastCopiedID { get; set; }
            public DateTime LastCollectTime { get; set; }
            public virtual Guid RouteID { get; set; }
            public virtual RouteEntityViewModel.Summary Route { get; set; }
        }
    }
}
