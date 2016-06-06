using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Nodes;

namespace PhotoMSK.ViewModels.Realisation
{
    class CheckoutService
    {

        
        public bool ProcessCheckout(RouteEntity route, UserInformation info, Order order)
        {
            new JintNode()
            {
                Script = @"
                    if(user.Penalties.length>0)
                        order.Status.ID
                    "
            };
            return true;
        }

    }
}
