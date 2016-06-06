using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ShoppingCart;

namespace PhotoMSK.ViewModels.Nodes
{
    class JintNode
    {
        public string Script { get; set; }

        public bool ProcessNode(RouteEntity route, UserInformation user, Order order)
        {

            var engine = new Engine()
                .SetValue("route", route)
                .SetValue("user", user)
                .SetValue("order", order);

            engine.Execute(Script);
            return true;
        }
    }
}
