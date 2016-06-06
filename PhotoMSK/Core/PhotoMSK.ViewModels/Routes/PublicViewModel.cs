using System.Collections.Generic;

namespace PhotoMSK.ViewModels.Route
{
    public static class PublicViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }
            public string City { get; set; }
            public override string RouteType => "Public";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public override string RouteType => "Public";

            public string Name { get; set; }
            public string City { get; set; }
            public UserInformationViewModel.Summary Creator { get; set; }
            public IList<UserInformationViewModel.Summary> Administrators { get; set; }
        }
    }
}