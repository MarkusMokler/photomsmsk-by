using System;

namespace PhotoMSK.ViewModels
{
    public static partial class UserMenuItemViewModel
    {
        public class Details
        {
            public Guid ID { get; set; }
            public int Index { get; set; }
            public string Icon { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string Title { get; set; }
            public string Shortcut { get; set; }
            public virtual UserViewModel.Summary User { get; set; }
        }
    }
}
