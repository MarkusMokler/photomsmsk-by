using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Fotobel.Classes;
using Microsoft.AspNet.SignalR;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Hubs
{
    public class CartHub : Hub
    {
        private readonly IEventLocker _locker;

        public CartHub(IEventLocker eventLocker)
        {
            _locker = eventLocker;
        }

        public void ClearCart()
        {
            var id = Context.User.GetUserInformationID();
            _locker.ClearLocks(id);
        }
    }
}