using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Data.Models.Routes
{
    public class RouteReview
    {
        public Guid ID { get; set; }
        public virtual UserInformation User { get; set; }
        public virtual RouteEntity Route { get; set; }
        public virtual LikeStatus LikeStatus { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GoodComment { get; set; }
        public string BadComment { get; set; }
    }
}
