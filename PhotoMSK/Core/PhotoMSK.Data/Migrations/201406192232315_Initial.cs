using System.Data.Entity.Migrations;

namespace PhotoMSK.Data.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Subtitle = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Views = c.Int(nullable: false),
                        Visits = c.Int(nullable: false),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        AllDay = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Url = c.String(),
                        ClassName = c.String(),
                        Editable = c.Boolean(nullable: false),
                        StartEditable = c.Boolean(nullable: false),
                        DurationEditable = c.Boolean(nullable: false),
                        Color = c.String(),
                        BackgroundColor = c.String(),
                        BorderColor = c.String(),
                        TextColor = c.String(),
                        UserInformationID = c.Guid(nullable: false),
                        Calendar_ID = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.Calendar_ID)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.UserInformationID)
                .Index(t => t.Calendar_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserInformations",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserPhoto = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Penalties",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paragraph = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        UserInformationID = c.Guid(nullable: false),
                        Event_ID = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.Event_ID)
                .ForeignKey("dbo.UserInformations", t => t.UserInformationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.UserInformationID)
                .Index(t => t.Event_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserPhones",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        Confirm = c.Boolean(nullable: false),
                        ConfirmCode = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Phones", t => t.ID)
                .ForeignKey("dbo.UserInformations", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoutesPhones",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EntityID = c.Guid(nullable: false),
                        PhoneID = c.Guid(nullable: false),
                        Confirm = c.Boolean(nullable: false),
                        ConfirmCode = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.Phones", t => t.PhoneID, cascadeDelete: true)
                .Index(t => t.EntityID)
                .Index(t => t.PhoneID);
            
            CreateTable(
                "dbo.RouteEntity",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Shortcut = c.String(maxLength: 25),
                        Description = c.String(),
                        Pro = c.Boolean(nullable: false),
                        Verified = c.Boolean(nullable: false),
                        LikeStatus_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LikeStatus", t => t.LikeStatus_ID)
                .Index(t => t.Shortcut, unique: true)
                .Index(t => t.LikeStatus_ID);
            
            CreateTable(
                "dbo.LikeStatus",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Creatives",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Photographer_ID = c.Guid(),
                        Genre_ID = c.Guid(nullable: false),
                        Photo_ID = c.Guid(nullable: false),
                        LikeStatus_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photographer", t => t.Photographer_ID)
                .ForeignKey("dbo.PhotoGeners", t => t.Genre_ID, cascadeDelete: true)
                .ForeignKey("dbo.Photos", t => t.Photo_ID)
                .ForeignKey("dbo.LikeStatus", t => t.LikeStatus_ID)
                .Index(t => t.Photographer_ID)
                .Index(t => t.Genre_ID)
                .Index(t => t.Photo_ID)
                .Index(t => t.LikeStatus_ID);
            
            CreateTable(
                "dbo.PhotoGeners",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Genre_ID = c.Guid(),
                        Photographer_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.Genre_ID)
                .ForeignKey("dbo.Photographer", t => t.Photographer_ID)
                .Index(t => t.Genre_ID)
                .Index(t => t.Photographer_ID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Snapshots",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SnapshotYear = c.DateTime(nullable: false),
                        Photo_ID = c.Guid(),
                        Photomodel_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photos", t => t.Photo_ID)
                .ForeignKey("dbo.Photomodel", t => t.Photomodel_ID)
                .Index(t => t.Photo_ID)
                .Index(t => t.Photomodel_ID);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        News_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.News", t => t.News_ID)
                .Index(t => t.News_ID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Desctiption = c.String(),
                        Date = c.DateTime(nullable: false),
                        LikeStatus_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LikeStatus", t => t.LikeStatus_ID)
                .Index(t => t.LikeStatus_ID);
            
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EntityID = c.Guid(nullable: false),
                        ParticipationType = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        News_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.EntityID, cascadeDelete: true)
                .ForeignKey("dbo.News", t => t.News_ID)
                .Index(t => t.EntityID)
                .Index(t => t.News_ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Like = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        News_ID = c.Guid(),
                        LikeStatus_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.News", t => t.News_ID)
                .ForeignKey("dbo.LikeStatus", t => t.LikeStatus_ID)
                .Index(t => t.User_Id)
                .Index(t => t.News_ID)
                .Index(t => t.LikeStatus_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AccessStatus = c.Int(nullable: false),
                        Route_ID = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.Route_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Route_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserMenuItems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Index = c.Int(nullable: false),
                        Icon = c.String(),
                        Action = c.String(),
                        Controller = c.String(),
                        Title = c.String(),
                        Shortcut = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Promocodes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Code = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Percentage = c.Double(nullable: false),
                        ExpiryDate = c.DateTime(),
                        OwnerEntity_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.OwnerEntity_ID)
                .Index(t => t.OwnerEntity_ID);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ParameterValues",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Parameter_ID = c.Guid(),
                        Phototechnics_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Parameters", t => t.Parameter_ID)
                .ForeignKey("dbo.Phototechnics", t => t.Phototechnics_ID)
                .Index(t => t.Parameter_ID)
                .Index(t => t.Phototechnics_ID);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PricePositions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Price = c.Double(nullable: false),
                        Photoshop_ID = c.Guid(),
                        Phototechnics_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photoshops", t => t.Photoshop_ID)
                .ForeignKey("dbo.Phototechnics", t => t.Phototechnics_ID)
                .Index(t => t.Photoshop_ID)
                .Index(t => t.Phototechnics_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        PhotostudioID = c.Guid(nullable: false),
                        Square = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Photostudio", t => t.PhotostudioID)
                .Index(t => t.PhotostudioID);
            
            CreateTable(
                "dbo.ScheduleDays",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        TimeStart = c.DateTime(nullable: false),
                        TimeEnd = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        HallID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.HallID, cascadeDelete: true)
                .Index(t => t.HallID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PhotomodelGenres",
                c => new
                    {
                        Photomodel_ID = c.Guid(nullable: false),
                        Genre_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photomodel_ID, t.Genre_ID })
                .ForeignKey("dbo.Photomodel", t => t.Photomodel_ID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_ID, cascadeDelete: true)
                .Index(t => t.Photomodel_ID)
                .Index(t => t.Genre_ID);
            
            CreateTable(
                "dbo.HallCalendars",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Hall_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.ID)
                .ForeignKey("dbo.Halls", t => t.Hall_ID)
                .Index(t => t.ID)
                .Index(t => t.Hall_ID);
            
            CreateTable(
                "dbo.Masterclass",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AuthorName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Photographer",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Photomodel",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Article_ID = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Chest = c.Double(nullable: false),
                        Waist = c.Double(nullable: false),
                        Hips = c.Double(nullable: false),
                        ClothingSize = c.Double(nullable: false),
                        HairColor = c.Int(nullable: false),
                        SkinColor = c.Int(nullable: false),
                        City = c.String(),
                        Age = c.Int(nullable: false),
                        HairLength = c.Int(nullable: false),
                        EyesColor = c.String(),
                        FaceType = c.String(),
                        ShoesSize = c.Int(nullable: false),
                        StartYear = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .ForeignKey("dbo.Articles", t => t.Article_ID)
                .Index(t => t.ID)
                .Index(t => t.Article_ID);
            
            CreateTable(
                "dbo.Photorent",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        City = c.String(),
                        MetroStation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RouteEntity_ID = c.Guid(),
                        Hall_ID = c.Guid(),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Attachments", t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.RouteEntity_ID)
                .ForeignKey("dbo.Halls", t => t.Hall_ID)
                .Index(t => t.ID)
                .Index(t => t.RouteEntity_ID)
                .Index(t => t.Hall_ID);
            
            CreateTable(
                "dbo.Photoshops",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        City = c.String(),
                        MetroStation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Photostudio",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        City = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Actuality = c.DateTime(nullable: false),
                        Reviews = c.Int(nullable: false),
                        MetroStation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Phototechnics",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Manufacture_ID = c.Guid(),
                        TechnicsType_ID = c.Guid(),
                        Format = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .ForeignKey("dbo.Brands", t => t.Manufacture_ID)
                .ForeignKey("dbo.Categories", t => t.TechnicsType_ID)
                .Index(t => t.ID)
                .Index(t => t.Manufacture_ID)
                .Index(t => t.TechnicsType_ID);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RouteEntity", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.RentCalendars",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Photorent_ID = c.Guid(),
                        Phototechnics_ID = c.Guid(),
                        Price = c.Double(nullable: false),
                        HollidayPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.ID)
                .ForeignKey("dbo.Photorent", t => t.Photorent_ID)
                .ForeignKey("dbo.Phototechnics", t => t.Phototechnics_ID)
                .Index(t => t.ID)
                .Index(t => t.Photorent_ID)
                .Index(t => t.Phototechnics_ID);
            
            CreateTable(
                "dbo.Shooting",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Gener_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.News", t => t.ID)
                .ForeignKey("dbo.PhotoGeners", t => t.Gener_ID)
                .Index(t => t.ID)
                .Index(t => t.Gener_ID);
            
            CreateTable(
                "dbo.Audios",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Attachments", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Poll",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Attachments", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Attachments", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.PhotographerCalendar",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Photographer_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Calendars", t => t.ID)
                .ForeignKey("dbo.Photographer", t => t.Photographer_ID)
                .Index(t => t.ID)
                .Index(t => t.Photographer_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotographerCalendar", "Photographer_ID", "dbo.Photographer");
            DropForeignKey("dbo.PhotographerCalendar", "ID", "dbo.Calendars");
            DropForeignKey("dbo.Videos", "ID", "dbo.Attachments");
            DropForeignKey("dbo.Poll", "ID", "dbo.Attachments");
            DropForeignKey("dbo.Audios", "ID", "dbo.Attachments");
            DropForeignKey("dbo.Shooting", "Gener_ID", "dbo.PhotoGeners");
            DropForeignKey("dbo.Shooting", "ID", "dbo.News");
            DropForeignKey("dbo.RentCalendars", "Phototechnics_ID", "dbo.Phototechnics");
            DropForeignKey("dbo.RentCalendars", "Photorent_ID", "dbo.Photorent");
            DropForeignKey("dbo.RentCalendars", "ID", "dbo.Calendars");
            DropForeignKey("dbo.Places", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Phototechnics", "TechnicsType_ID", "dbo.Categories");
            DropForeignKey("dbo.Phototechnics", "Manufacture_ID", "dbo.Brands");
            DropForeignKey("dbo.Phototechnics", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photostudio", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photoshops", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photos", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.Photos", "RouteEntity_ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photos", "ID", "dbo.Attachments");
            DropForeignKey("dbo.Photorent", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photomodel", "Article_ID", "dbo.Articles");
            DropForeignKey("dbo.Photomodel", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Photographer", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Pages", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.Masterclass", "ID", "dbo.RouteEntity");
            DropForeignKey("dbo.HallCalendars", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.HallCalendars", "ID", "dbo.Calendars");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserInformations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPhones", "UserID", "dbo.UserInformations");
            DropForeignKey("dbo.UserPhones", "ID", "dbo.Phones");
            DropForeignKey("dbo.RoutesPhones", "PhoneID", "dbo.Phones");
            DropForeignKey("dbo.RoutesPhones", "EntityID", "dbo.RouteEntity");
            DropForeignKey("dbo.Halls", "PhotostudioID", "dbo.Photostudio");
            DropForeignKey("dbo.ScheduleDays", "HallID", "dbo.Halls");
            DropForeignKey("dbo.PricePositions", "Phototechnics_ID", "dbo.Phototechnics");
            DropForeignKey("dbo.PricePositions", "Photoshop_ID", "dbo.Photoshops");
            DropForeignKey("dbo.ParameterValues", "Phototechnics_ID", "dbo.Phototechnics");
            DropForeignKey("dbo.ParameterValues", "Parameter_ID", "dbo.Parameters");
            DropForeignKey("dbo.Promocodes", "OwnerEntity_ID", "dbo.RouteEntity");
            DropForeignKey("dbo.RouteEntity", "LikeStatus_ID", "dbo.LikeStatus");
            DropForeignKey("dbo.Reviews", "LikeStatus_ID", "dbo.LikeStatus");
            DropForeignKey("dbo.News", "LikeStatus_ID", "dbo.LikeStatus");
            DropForeignKey("dbo.Reviews", "News_ID", "dbo.News");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserMenuItems", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Roles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Roles", "Route_ID", "dbo.RouteEntity");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Penalties", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Views", "News_ID", "dbo.News");
            DropForeignKey("dbo.Views", "EntityID", "dbo.RouteEntity");
            DropForeignKey("dbo.Attachments", "News_ID", "dbo.News");
            DropForeignKey("dbo.Creatives", "LikeStatus_ID", "dbo.LikeStatus");
            DropForeignKey("dbo.Creatives", "Photo_ID", "dbo.Photos");
            DropForeignKey("dbo.Creatives", "Genre_ID", "dbo.PhotoGeners");
            DropForeignKey("dbo.PhotoGeners", "Photographer_ID", "dbo.Photographer");
            DropForeignKey("dbo.Creatives", "Photographer_ID", "dbo.Photographer");
            DropForeignKey("dbo.PhotoGeners", "Genre_ID", "dbo.Genres");
            DropForeignKey("dbo.Snapshots", "Photomodel_ID", "dbo.Photomodel");
            DropForeignKey("dbo.Snapshots", "Photo_ID", "dbo.Photos");
            DropForeignKey("dbo.PhotomodelGenres", "Genre_ID", "dbo.Genres");
            DropForeignKey("dbo.PhotomodelGenres", "Photomodel_ID", "dbo.Photomodel");
            DropForeignKey("dbo.Penalties", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.Penalties", "Event_ID", "dbo.Events");
            DropForeignKey("dbo.Events", "UserInformationID", "dbo.UserInformations");
            DropForeignKey("dbo.Events", "Calendar_ID", "dbo.Calendars");
            DropIndex("dbo.PhotographerCalendar", new[] { "Photographer_ID" });
            DropIndex("dbo.PhotographerCalendar", new[] { "ID" });
            DropIndex("dbo.Videos", new[] { "ID" });
            DropIndex("dbo.Poll", new[] { "ID" });
            DropIndex("dbo.Audios", new[] { "ID" });
            DropIndex("dbo.Shooting", new[] { "Gener_ID" });
            DropIndex("dbo.Shooting", new[] { "ID" });
            DropIndex("dbo.RentCalendars", new[] { "Phototechnics_ID" });
            DropIndex("dbo.RentCalendars", new[] { "Photorent_ID" });
            DropIndex("dbo.RentCalendars", new[] { "ID" });
            DropIndex("dbo.Places", new[] { "ID" });
            DropIndex("dbo.Phototechnics", new[] { "TechnicsType_ID" });
            DropIndex("dbo.Phototechnics", new[] { "Manufacture_ID" });
            DropIndex("dbo.Phototechnics", new[] { "ID" });
            DropIndex("dbo.Photostudio", new[] { "ID" });
            DropIndex("dbo.Photoshops", new[] { "ID" });
            DropIndex("dbo.Photos", new[] { "Hall_ID" });
            DropIndex("dbo.Photos", new[] { "RouteEntity_ID" });
            DropIndex("dbo.Photos", new[] { "ID" });
            DropIndex("dbo.Photorent", new[] { "ID" });
            DropIndex("dbo.Photomodel", new[] { "Article_ID" });
            DropIndex("dbo.Photomodel", new[] { "ID" });
            DropIndex("dbo.Photographer", new[] { "ID" });
            DropIndex("dbo.Pages", new[] { "ID" });
            DropIndex("dbo.Masterclass", new[] { "ID" });
            DropIndex("dbo.HallCalendars", new[] { "Hall_ID" });
            DropIndex("dbo.HallCalendars", new[] { "ID" });
            DropIndex("dbo.PhotomodelGenres", new[] { "Genre_ID" });
            DropIndex("dbo.PhotomodelGenres", new[] { "Photomodel_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ScheduleDays", new[] { "HallID" });
            DropIndex("dbo.Halls", new[] { "PhotostudioID" });
            DropIndex("dbo.PricePositions", new[] { "Phototechnics_ID" });
            DropIndex("dbo.PricePositions", new[] { "Photoshop_ID" });
            DropIndex("dbo.ParameterValues", new[] { "Phototechnics_ID" });
            DropIndex("dbo.ParameterValues", new[] { "Parameter_ID" });
            DropIndex("dbo.Promocodes", new[] { "OwnerEntity_ID" });
            DropIndex("dbo.UserMenuItems", new[] { "User_Id" });
            DropIndex("dbo.Roles", new[] { "User_Id" });
            DropIndex("dbo.Roles", new[] { "Route_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Reviews", new[] { "LikeStatus_ID" });
            DropIndex("dbo.Reviews", new[] { "News_ID" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Views", new[] { "News_ID" });
            DropIndex("dbo.Views", new[] { "EntityID" });
            DropIndex("dbo.News", new[] { "LikeStatus_ID" });
            DropIndex("dbo.Attachments", new[] { "News_ID" });
            DropIndex("dbo.Snapshots", new[] { "Photomodel_ID" });
            DropIndex("dbo.Snapshots", new[] { "Photo_ID" });
            DropIndex("dbo.PhotoGeners", new[] { "Photographer_ID" });
            DropIndex("dbo.PhotoGeners", new[] { "Genre_ID" });
            DropIndex("dbo.Creatives", new[] { "LikeStatus_ID" });
            DropIndex("dbo.Creatives", new[] { "Photo_ID" });
            DropIndex("dbo.Creatives", new[] { "Genre_ID" });
            DropIndex("dbo.Creatives", new[] { "Photographer_ID" });
            DropIndex("dbo.RouteEntity", new[] { "LikeStatus_ID" });
            DropIndex("dbo.RouteEntity", new[] { "Shortcut" });
            DropIndex("dbo.RoutesPhones", new[] { "PhoneID" });
            DropIndex("dbo.RoutesPhones", new[] { "EntityID" });
            DropIndex("dbo.UserPhones", new[] { "UserID" });
            DropIndex("dbo.UserPhones", new[] { "ID" });
            DropIndex("dbo.Penalties", new[] { "User_Id" });
            DropIndex("dbo.Penalties", new[] { "Event_ID" });
            DropIndex("dbo.Penalties", new[] { "UserInformationID" });
            DropIndex("dbo.UserInformations", new[] { "User_Id" });
            DropIndex("dbo.Events", new[] { "User_Id" });
            DropIndex("dbo.Events", new[] { "Calendar_ID" });
            DropIndex("dbo.Events", new[] { "UserInformationID" });
            DropTable("dbo.PhotographerCalendar");
            DropTable("dbo.Videos");
            DropTable("dbo.Poll");
            DropTable("dbo.Audios");
            DropTable("dbo.Shooting");
            DropTable("dbo.RentCalendars");
            DropTable("dbo.Places");
            DropTable("dbo.Phototechnics");
            DropTable("dbo.Photostudio");
            DropTable("dbo.Photoshops");
            DropTable("dbo.Photos");
            DropTable("dbo.Photorent");
            DropTable("dbo.Photomodel");
            DropTable("dbo.Photographer");
            DropTable("dbo.Pages");
            DropTable("dbo.Masterclass");
            DropTable("dbo.HallCalendars");
            DropTable("dbo.PhotomodelGenres");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ScheduleDays");
            DropTable("dbo.Halls");
            DropTable("dbo.Categories");
            DropTable("dbo.PricePositions");
            DropTable("dbo.Parameters");
            DropTable("dbo.ParameterValues");
            DropTable("dbo.Brands");
            DropTable("dbo.Promocodes");
            DropTable("dbo.UserMenuItems");
            DropTable("dbo.Roles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Views");
            DropTable("dbo.News");
            DropTable("dbo.Attachments");
            DropTable("dbo.Snapshots");
            DropTable("dbo.Articles");
            DropTable("dbo.Genres");
            DropTable("dbo.PhotoGeners");
            DropTable("dbo.Creatives");
            DropTable("dbo.LikeStatus");
            DropTable("dbo.RouteEntity");
            DropTable("dbo.RoutesPhones");
            DropTable("dbo.Phones");
            DropTable("dbo.UserPhones");
            DropTable("dbo.Penalties");
            DropTable("dbo.UserInformations");
            DropTable("dbo.Events");
            DropTable("dbo.Calendars");
            DropTable("dbo.Ads");
        }
    }
}
