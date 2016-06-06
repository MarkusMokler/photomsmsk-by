using System.Collections.Generic;

namespace PhotoMSK.ViewModels.Routes
{
    public static class PhotoshopViewModel
    {
        public class Details : RouteEntityViewModel.Details
        {
            public string Name { get; set; }
            public string City { get; set; }
            public string MetroStation { get; set; }
            public double CurencyValue { get; set; }
            public UserInformationViewModel.Summary Creator { get; set; }
            public IList<UserInformationViewModel.Summary> Administrators { get; set; }
            public override string RouteType => "Photoshop";


        }
        public class Summary : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }

            public string City { get; set; }

            public string MetroStation { get; set; }
            public double CurencyValue { get; set; }
            public override string RouteType => "Photoshop";
        }

        public class WhiteLabel : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }
            public override string RouteType => "Photoshop";
        }
    }
}