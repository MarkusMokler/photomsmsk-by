using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;
using System;

namespace PhotoMSK.ViewModels.Routes.Photographer
{
    public static class ProjectViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid PhotographerID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Description { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public Guid PhotographerID { get; set; }
            public Guid ProjectSettings { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Description { get; set; }
            public string LandingPageUrl { get; set; }
            public string Type { get; set; }
            public string TeaserImageUrl { get; set; }
            public string CoverImageUrl { get; set; }
        }
    }

    public class ProjectViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Project, ProjectViewModel.Summary>();
            CreateMap<Project, ProjectViewModel.Details>();
        }
    }
}
