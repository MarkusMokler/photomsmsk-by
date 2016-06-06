using System.Collections.Generic;

namespace PhotoMSK.ViewModels.Routes
{
    public static class PhotographerViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public double RaitingTotal { get; set; }
            public override string RouteType => "Photographer";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public override string RouteType => "Photographer";

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name { get; set; }

            public string City { get; set; }

            public double CurrencyValue { get; set; }
            public UserInformationViewModel.Summary Creator { get; set; }
            public IList<UserInformationViewModel.Summary> Administrators { get; set; }
            public IList<string> Geners { get; set; }
            public int Subscribers { get; set; }
        }

    }
}