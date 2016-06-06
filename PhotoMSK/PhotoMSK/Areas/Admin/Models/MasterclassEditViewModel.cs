using System;
using System.ComponentModel.DataAnnotations;
using PhotoMSK.Models.Attributes;
using PhotoMSK.Models.EditViewModel.Routes;


namespace PhotoMSK.Areas.Admin.Models
{
    public class MasterclassEditViewModel : RouteEditModel
    {
        [RequiredPost]
        [RegularExpression("[А-яA-z 0-9,.-]{3,25}$")]
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string AuthorName { get; set; }
        public DateTime StartDay { get; set; }
        public int Days { get; set; }
        public string Listeners { get; set; } //изменить тип на int
       /* public string AuthorLastName { get; set; }
          public string City { get; set; }
          public string VisitingCity { get; set; }
          public double? Latitude { get; set; }
          public double? Longitude { get; set; }*/
    }
}