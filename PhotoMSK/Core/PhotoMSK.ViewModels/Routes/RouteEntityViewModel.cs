using System;
using System.Collections.Generic;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class RouteEntityViewModel
    {
        public abstract class Summary : UniqueEntity
        {
            public string Url { get; set; }
            public string Shortcut { get; set; }
            public string Description { get; set; }
            public bool Pro { get; set; }
            public bool Verified { get; set; }
            public string Site { get; set; }
            public virtual string TeaserImage { get; set; }
            public virtual string CoverImage { get; set; }
            public int PageViews { get; protected set; }
            public virtual string FriendlyRoute { get; set; }
            public bool AllowOnlineBooking { get; set; }
            public DateTime CreationTime { get; set; }

            public abstract string RouteType { get; }

            #region SEO
            public string SeoTitle { get; set; }
            public string SeoDescription { get; set; }
            public string SeoKeywords { get; set; }
            #endregion

            #region ЮрДанные
            public string Legaladdress { get; set; } //юридический адрес
            public string AccountNumber { get; set; } //УНП
            public string RegisterTrade { get; set; } //ЗАРЕГИСТРИРОВАН В ТОРГОВОМ РЕЕСТРЕ
            public string СertificateNumber { get; set; } //НОМЕР СВИДЕТЕЛЬСТВА О РЕГИСТРАЦИИ
            public DateTime? RegisterDate { get; set; } //ВЫДАНО СВИДЕТЕЛЬСТВО О РЕГИСТРАЦИИ(дата)
            public string RegisteringAgency { get; set; } //РЕГИСТРИРУЮЩИЙ ОРГАН  

            #endregion

            #region Социальные
            public virtual string Facebook { get; set; }
            public virtual string Vk { get; set; }
            public virtual string Instagram { get; set; }
            public virtual string Twitter { get; set; }
            public virtual string Skype { get; set; }
            #endregion

            #region WhiteLabel
            public virtual bool WhiteLabel { get; set; }
            public virtual string Domain { get; set; }
            public virtual string Theme { get; set; }
            #endregion

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

        public abstract class Details : Summary
        {
            public virtual LikeStatusViewModel.Summary LikeStatus { get; set; }
            public virtual RaitingViewModel.Summary Raiting { get; set; }
            public virtual IList<WallPostViewModel.Details> Wall { get; set; }
            public virtual ICollection<PhotoViewModel.Summary> Photos { get; set; }
            public virtual ICollection<RoleViewModel.Summary> Participants { get; set; }
            public virtual ICollection<PromocodeViewModel.Summary> Promocodes { get; set; }
            public virtual ICollection<TextPageViewModel.Summary> Pages { get; set; }
            public ICollection<PageCategoryViewModel.Summary> PageCategories { get; set; }
        }
    }

    public interface IWall : IEnumerable<WallPostViewModel.Details>
    {
    }
}
