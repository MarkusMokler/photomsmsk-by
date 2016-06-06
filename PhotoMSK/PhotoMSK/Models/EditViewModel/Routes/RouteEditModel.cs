using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Models.Attributes;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Models.EditViewModel.Routes
{
    public class RouteEditModel : AbstractEditModel
    {
        [RequiredPost]
        [Shortcut]
        [RegularExpression("[А-яa-z0-9-]{3,15}$", ErrorMessage = "Неправильная короткая ссылка")]
        public string Shortcut { get; set; }

        public string Description { get; set; }

        [RegularExpression(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
            ErrorMessage = "Неправильный адрес сайта")]
        public string Site { get; set; }

        public string TeaserImage { get; set; }
        public string CoverImage { get; set; }
       
        #region SEO
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        #endregion

        #region Социальные
        public virtual string Facebook { get; set; }
        public virtual string Vk { get; set; }
        public virtual string Instagram { get; set; }
        public virtual string Twitter { get; set; }
        public virtual string Skype { get; set; }
        #endregion

        #region WhiteLabel
        public string Domain { get; set; }
        public string Theme { get; set; }
        public IEnumerable<UserInformationEditModel> Participants { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
        public string Name { get; set; }
     
        #endregion

        public virtual string FriendlyRoute { get; set; }
        public bool AllowOnlineBooking { get; set; }
     
        public virtual string ImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(TeaserImage) == false
                    ? TeaserImage
                    : "/Content/images/no-avatar.png";
            }
        }
        public virtual ICollection<PhoneViewModel.Summary> Phones { get; set; }
        public virtual string CoverImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(CoverImage) == false
                    ? CoverImage
                    : "/Content/images/CoverImage1.jpg";
            }
        }
        
    }
}