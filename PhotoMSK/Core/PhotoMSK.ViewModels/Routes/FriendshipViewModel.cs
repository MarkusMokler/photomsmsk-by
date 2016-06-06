using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Routes
{
    public static class FriendshipViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid ChildRouteID { get; set; }
            public string Name { get; set; }
            public string Photo { get; set; }
            public FriendshipType FriendshipType { get; set; }

        }

        public class RoutesMapping : MapperProfile
        {
            /// <summary>
            /// Override this method in a derived class and call the CreateMap method to associate that map with thisMapperProfile.
            ///             Avoid calling the <see cref="T:AutoMapper.Mapper"/> class from this method.
            /// </summary>
            protected override void Configure()
            {
                CreateMap<Friendship, FriendshipViewModel.Summary>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(z => z.ChildRoute.GetName()))
                    .ForMember(x => x.Photo, opt => opt.MapFrom(z => z.ChildRoute.TeaserImage));
            }
        }
    }
}
