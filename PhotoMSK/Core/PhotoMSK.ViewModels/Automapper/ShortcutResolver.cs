using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.ViewModels.Automapper
{
    class ShortcutResolver : ValueResolver<RouteEntity, string>
    {
        private readonly IUrlBuilder _urlBuilder;

        public ShortcutResolver(IUrlBuilder urlBuilder)
        {
            _urlBuilder = urlBuilder;
        }

        protected override string ResolveCore(RouteEntity source)
        {
            return _urlBuilder.GetShortcutUrl(source.Shortcut);
        }
    }
}
