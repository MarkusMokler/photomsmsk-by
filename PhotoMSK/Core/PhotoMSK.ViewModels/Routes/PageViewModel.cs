using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.ViewModels.Route
{
    public class PageViewModel
    {
        public class Summary : RouteEntityViewModel.Summary
        {
            public string Name { get; set; }
            public override string RouteType => "Page";
        }

        public class Details : RouteEntityViewModel.Details
        {
            public string Name { get; set; }
            public override string RouteType => "Page";

        }
    }
}
