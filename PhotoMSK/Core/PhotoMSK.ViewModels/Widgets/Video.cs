using System;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class VideoWidget
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public Guid? VideoID { get; set; }
            public virtual Video Video { get; set; }
        }
    }
}