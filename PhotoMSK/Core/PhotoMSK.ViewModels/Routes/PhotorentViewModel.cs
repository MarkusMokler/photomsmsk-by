using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Automapper;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Route
{
    public static class PhotorentViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }

            public string City { get; set; }

            public double CurrencyValue { get; set; }
            public override string RouteType => "Photorent";
        }
        public class Details : RouteEntityViewModel.Details
        {
            public override string RouteType => "Photorent";

            public string Name { get; set; }

            public string City { get; set; }
            public string Adress { get; set; }
            public double CurrencyValue { get; set; }
            public UserInformationViewModel.Summary Creator { get; set; }
            public IList<UserInformationViewModel.Summary> Administrators { get; set; }
        }

        
    }
}