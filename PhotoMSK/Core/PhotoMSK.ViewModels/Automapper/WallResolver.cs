using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;

namespace PhotoMSK.ViewModels.Automapper
{
    public class WallResolver : ValueResolver<RouteEntity, IWall>
    {
        private readonly IUserInformationProvider _informationProvider;
        private readonly IUrlBuilder _urlBuilder;

        public WallResolver(IUserInformationProvider informationProvider, IUrlBuilder urlBuilder)
        {
            _informationProvider = informationProvider;
            _urlBuilder = urlBuilder;
        }

        protected override IWall ResolveCore(RouteEntity source)
        {
            return new WallService(source.ID,_urlBuilder);
        }
    }
}
