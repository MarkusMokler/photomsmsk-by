using System;
using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Routes
{
    public class Masterclass : RouteEntity
    {
        public string Title { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Days { get; set; }
        public string Listeners { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
        public string VisitingCity { get; set; }
        public virtual ICollection<MasterclassEvents> Events { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public override string GetName()
        {
            return Title;
        }
    }
}