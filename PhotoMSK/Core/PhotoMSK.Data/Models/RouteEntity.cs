using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Clients;
using PhotoMSK.Data.Models.Permission;

namespace PhotoMSK.Data.Models
{
    public abstract class RouteEntity : UniqueEntity
    {
        protected RouteEntity()
        {
            CreationTime = DateTime.Now;
        }

        [Index(IsUnique = true)]
        [MaxLength(35)]
        public string Shortcut { get; set; }
        public abstract string GetName();
        public string Description { get; set; }
        public bool Pro { get; set; }
        public bool Verified { get; set; }
        public string Site { get; set; }
        public virtual LikeStatus LikeStatus { get; set; }
        public virtual string TeaserImage { get; set; }
        public virtual string CoverImage { get; set; }
        public int PageViews { get; protected set; }
        public virtual string FriendlyRoute { get; set; }

        public bool AllowOnlineBooking => LegalInformation?.AllowOnlineBooking ?? false;

        public DateTime CreationTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int LastDocumentCode { get; set; }

        public Guid? MainPageID { get; set; }
        public virtual BasePage MainPage { get; set; }

        public Guid? DefaultRoutePageLayoutID { get; set; }
        public virtual RoutepageLayout DefaultRoutePageLayout { get; set; }

        public virtual ICollection<RouteSetting> RouteSettings { get; set; }
        public virtual ICollection<ThemeStyles> Styles { get; set; }
        public void AddContract(Contract contract)
        {
            LastDocumentCode++;
            contract.BarCode = LastDocumentCode;
            Contracts.Add(contract);
        }

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
        public virtual bool WhiteLabel { get; set; }
        public virtual string Domain { get; set; }
        public virtual string Theme { get; set; }
        #endregion


        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Address { get; set; }
        public string MetroStation { get; set; }
        public bool Active { get; set; }


        public virtual LegalInformation LegalInformation { get; set; }

        public virtual string ImageUrl => string.IsNullOrEmpty(TeaserImage) == false
            ? TeaserImage
            : "/Content/images/no-avatar.png";

        public virtual string CoverImageUrl => string.IsNullOrEmpty(CoverImage) == false
            ? CoverImage
            : "/Content/images/CoverImage1.jpg";

        public virtual Guid? RootDirectoryID { get; set; }
        public virtual FileEntry RootDirectory { get; set; }
        public virtual Raiting Raiting { get; set; }
        public virtual ICollection<RoutesPhone> Phones { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

        public virtual ICollection<Views> Wall { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Role> Participants { get; set; }
        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<BasePage> Pages { get; set; }
        public ICollection<PageCategory> PageCategories { get; set; }
        public virtual ICollection<RouteClientCategory> RouteClientCategories { get; set; }
        public SmsModule Sms { get; set; }

        public IEnumerable<Phone> GetPhones()
        {
            return Phones.Select(x => x.Phone);
        }

        public void AddView()
        {
            if (Raiting == null)
                Raiting = new Raiting();
            PageViews++;
            Raiting.AddView();
        }

        public void AddWallPost(Views views)
        {
            Wall.Add(views);
            Raiting.AddWallPost();
        }

        public void AddLike(LikeType likeType, LikeDetails exist = LikeDetails.Voited)
        {
            if (likeType == LikeType.Like)
            {
                Raiting.AddLike(exist);
            }
            else
            {
                Raiting.AddDislike(exist);
            }
        }

        #region permission
        /*Maching permissions

        http://blog.falafel.com/entity-framework-enum-flags/

        // Only return records that have ALL specified colors
        ColorFlags colorsToMatch = ColorFlags.Blue | ColorFlags.Red;
        var matchingColors = db.Cars.Where(c => c.Color & colorsToMatch == colorsToMatch);
        

        
        // Return records that have ANY of the specified colors
        ColorFlags colorsToMatch = ColorFlags.Blue | ColorFlags.Red;
        var matchingColors = db.Cars.Where(c => c.Color & colorsToMatch != 0);
        
        
        */

        public bool IsActionPermited(EventManage fullPatch, Guid uid)
        {
            var role = Participants.FirstOrDefault(x => x.UserInformation.ID == uid);
            if (role == null)
                return false;

            return role.AccessStatus == AccessStatus.Administrator || role.AccessStatus == AccessStatus.Owner;
        }
        public bool IsActionPermited(AccountManage fullPatch, Guid uid)
        {
            var role = Participants.FirstOrDefault(x => x.UserInformation.ID == uid);

            return role?.AccessStatus == AccessStatus.Owner;
        }

        #endregion

    }
}