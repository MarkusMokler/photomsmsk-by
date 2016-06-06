#region usings

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.Data.Models.Advertising;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Clients;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.Data.Models.Statistics;
using PhotoMSK.Data.Models.Widgets;

#endregion

namespace PhotoMSK.Data
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class AppContext : IdentityDbContext<User>
    {
        public AppContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<DeleteRouteRequest> DeleteRouteRequests { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Masterclass> Masterclasses { get; set; }
        public DbSet<Photographer> Photographers { get; set; }
        public DbSet<Photomodel> Photomodels { get; set; }
        public DbSet<Photorent> Photorents { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Photostudio> Photostudios { get; set; }
        public DbSet<Phototechnics> Phototechnicses { get; set; }
        public DbSet<RentCalendar> RentCalendars { get; set; }
        public DbSet<RouteEntity> Routes { get; set; }
        public DbSet<Shooting> Shootings { get; set; }
        public DbSet<Snapshot> Snapshots { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<WallPost> Newses { get; set; }
        public DbSet<Views> Viewses { get; set; }
        public DbSet<PricePosition> PricePositions { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photoshop> Photoshops { get; set; }
        public DbSet<DressRent> DressRents { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Role> Participants { get; set; }
        public DbSet<RoutesPhone> RoutesPhones { get; set; }
        public DbSet<HallCalendar> HallCalendars { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<ScheduleDay> ScheduleDays { get; set; }
        public DbSet<Models.Routes.Page> Pages { get; set; }
        public DbSet<SmsMessage> SmsMessages { get; set; }
        public DbSet<CopyReference> CopyReferenses { get; set; }
        public DbSet<CalendarReference> CalendarReference { get; set; }
        public DbSet<AdCompany> AdCompanies { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SaleCard> SaleCards { get; set; }
        public DbSet<GoogleSheetsSync> GoogleSheetsSync { get; set; }
        public DbSet<HallProperty> HallProperties { get; set; }
        public DbSet<BasePage> BasePages { get; set; }
        public DbSet<PageCategory> PageCategories { get; set; }
        public DbSet<AbstractMenuItem> AbstractMenuItem { get; set; }
        public DbSet<LinkMenuItem> LinkMenuItems { get; set; }
        public DbSet<PageMenuItem> PageMenuItems { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<HtmlTemplate> Razorviews { get; set; }
        public DbSet<RouteReview> RouteReviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FileEntry> FileEntries { get; set; }
        public DbSet<Friendship> Friends { get; set; }
        public DbSet<Zone> LandingPages { get; set; }
        public DbSet<BalanceLine> BalanceLines { get; set; }
        public DbSet<WeekBalance> WeekBalances { get; set; }
        public DbSet<Layout> Layouts { get; set; }

        public DbSet<Month> Months { get; set; }
        public DbSet<NominationPhoto> NominationPhotos { get; set; }
        public DbSet<Compare> Compares { get; set; }

        public DbSet<CallActivity> CallActivities { get; set; }
        public DbSet<EventActivity> EventActivities { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<RoutepageLayout> RoutePageLayouts { get; set; }
        public DbSet<BaseWidget> Widgets { get; set; }
        public DbSet<MenuWidget> MenuWidgets { get; set; }
        public DbSet<RouteMenu> RouteMenus { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSettings> ProjectSettings { get; set; }
        public DbSet<ThemeStyles> ThemeStyleses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoutepageLayout>().ToTable("RoutepageLayouts");

            modelBuilder.Entity<RouteEntity>().ToTable("RouteEntity")
                .HasOptional(x => x.DefaultRoutePageLayout).WithMany().HasForeignKey(x => x.DefaultRoutePageLayoutID);

            modelBuilder.Entity<RouteEntity>().HasOptional(x => x.MainPage).WithMany().HasForeignKey(x => x.MainPageID);
            modelBuilder.Entity<RouteEntity>().HasOptional(x => x.LegalInformation).WithRequired(x => x.RouteEntity);
            modelBuilder.Entity<RouteEntity>().HasOptional(x => x.Sms).WithRequired(x => x.RouteEntity);

            modelBuilder.Entity<Penalty>().ToTable("Penalties");

            modelBuilder.Entity<Photographer>().ToTable("Photographer");

            modelBuilder.Entity<Photomodel>().ToTable("Photomodel");

            modelBuilder.Entity<Photorent>().ToTable("Photorent");

            modelBuilder.Entity<ThemeStyles>().ToTable("ThemeStyles");

            modelBuilder.Entity<Layout>().ToTable("Layouts");

            modelBuilder.Entity<Photostudio>().ToTable("Photostudio");

            modelBuilder.Entity<Photostudio>()
                .HasMany(x => x.Halls)
                .WithRequired(x => x.Photostudio)
                .HasForeignKey(x => x.PhotostudioID);

            modelBuilder.Entity<Photoshop>().ToTable("Photoshops");

            modelBuilder.Entity<Masterclass>().ToTable("Masterclass");

            modelBuilder.Entity<Page>().ToTable("Pages");

            modelBuilder.Entity<Place>().ToTable("Places");

            modelBuilder.Entity<Attachment>().ToTable("Attachments");

            modelBuilder.Entity<Attachment>().HasKey(x => x.ID);
            modelBuilder.Entity<Attachment>().Property(x => x.WallPostID).HasColumnName("WallPost_ID");
            modelBuilder.Entity<Attachment>().HasOptional(x => x.Post).WithMany(x => x.Attacments).HasForeignKey(x => x.WallPostID);

            modelBuilder.Entity<Audio>().ToTable("Audios");

            modelBuilder.Entity<Photo>().ToTable("Photos");

            modelBuilder.Entity<Poll>().ToTable("Poll");

            modelBuilder.Entity<Video>().ToTable("Videos");

            modelBuilder.Entity<Hall>().HasMany(x => x.Schedule)
                .WithRequired(x => x.Hall).HasForeignKey(x => x.HallID);

            modelBuilder.Entity<Calendar>().ToTable("Calendars");

            modelBuilder.Entity<HallCalendar>().ToTable("HallCalendars");

            modelBuilder.Entity<PhotographerCalendar>().ToTable("PhotographerCalendar");

            modelBuilder.Entity<Photographer>()
                .HasOptional(x => x.Calendar).WithOptionalPrincipal(x => x.Photographer);

            modelBuilder.Entity<Hall>().HasOptional(x => x.Calendar)
                .WithOptionalPrincipal(x => x.Hall);

            modelBuilder.Entity<RouteReview>().ToTable("RouteReviews");

            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public void WallPostAddView(IList<Guid> ids)
        {
            if (ids.Count < 1)
                return;
            var idss = string.Join(",", ids.Select(x => string.Format("'{0}'", x)));
            Database.ExecuteSqlCommand(string.Format("UPDATE dbo.WallPosts SET Views = Views+1 WHERE ID in ({0})", idss));
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}