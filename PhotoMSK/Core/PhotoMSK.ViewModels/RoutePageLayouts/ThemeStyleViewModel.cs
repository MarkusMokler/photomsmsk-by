using PhotoMSK.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.RoutePageLayouts
{
    public class ThemeStyleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
    }

    public class ThemeStyleViewModelProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ThemeStyles, ThemeStyleViewModel>();
        }
    }
}
