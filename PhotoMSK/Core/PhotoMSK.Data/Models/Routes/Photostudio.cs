using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Routes
{
    public class Photostudio : RouteEntity
    {
        public Photostudio()
        {
            Actuality = DateTime.Now;
        }
        public string Name { get; set; }
        public DateTime Actuality { get; set; }
        public int Reviews { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public string Adress { get; set; }
        public int HallCount { get; set; }
        public int MinBookingTime { get; set; }
        public int BookingTimeInc { get; set; }
        public int DaysOfClaim { get; set; }

        public override string GetName()
        {
            return Name;
        }
        public object StudioType { get; set; }
        public string GetLastImage()
        {
            Photo photo = Photos.OrderByDescending(x => x.UploadDate).FirstOrDefault();

            if (photo != null)
                return photo.Url;

            return "/Content/images/no-avatar.png";
        }
        public override string ToString()
        {
            return Name;
        }
        //public virtual Role GetUserRole(string uName)
        //{
        //    return Participants.SingleOrDefault(x => x.UserInformation.User.UserName == uName);
        //}
    }
}