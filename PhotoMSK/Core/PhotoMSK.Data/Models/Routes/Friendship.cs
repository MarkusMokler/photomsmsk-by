using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Routes
{
    public class Friendship : UniqueEntity
    {
        public Guid BaseRouteID { get; set; }
        public virtual RouteEntity BaseRoute { get; set; }
        public Guid ChildRouteID { get; set; }
        public virtual RouteEntity ChildRoute { get; set; }

        public FriendshipType FriendshipType { get; set; }
    }

    public enum FriendshipType
    {
        Departament, Subdivision, Service
    }
}
