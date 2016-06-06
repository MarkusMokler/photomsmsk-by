using System;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Attachments;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class TextAdnDescriptionWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "description";
            public Guid? PhotoID { get; set; }
            public virtual PhotoViewModel.Details Photo { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}