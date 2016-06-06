using System.Collections.Generic;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class LikeStatusViewModel
    {
        public class Details
        {
            public virtual WallPostViewModel.Summary WallPost { get; set; }
            public virtual CreativeViewModel.Summary Creative { get; set; }
            public virtual ICollection<ReviewViewModel.Summary> Reviews { get; set; }
            public int Likes { get; protected set; }
            public int Dislikes { get; protected set; }
        }
    }
}
