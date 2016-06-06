using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.RoutePageLayouts;
using PhotoMSK.ViewModels.Widgets;
using FileGallery = PhotoMSK.Data.Models.Widgets.FileGallery;

namespace Fotobel.Api.Version2
{
    public class RoutePageLayoutsController : ApiController
    {
        AppContext _db = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var rpLayouts =
                _db.RoutePageLayouts.Where(x => x.Route.Shortcut.Equals(shortcut)).ToList();

            return Ok(rpLayouts.As<IList<RoutePageLayoutsViewModel.Details>>());
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut, Guid rpId)
        {
            var rpLayouts = _db.RoutePageLayouts.Find(rpId);

            return Ok(rpLayouts.As<RoutePageLayoutsViewModel.Details>());
        }

        [HttpPut]
        [Message("Слой установлен по умолчанию")]
        public IHttpActionResult Put(string shortcut, RoutePageLayoutsViewModel.Details routePage)
        {
            var route = _db.Routes.Single(x => x.Shortcut.Equals(shortcut));
            route.DefaultRoutePageLayout = _db.RoutePageLayouts.Find(routePage.ID);
            route.DefaultRoutePageLayoutID = routePage.ID;
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Message("Добавлено")]
        public IHttpActionResult Post(string shortcut, RoutePageLayoutsViewModel.Summary rpLayout)
        {
            var route = _db.Routes.Single(x => x.Shortcut.Equals(shortcut));
            var layout = _db.Layouts.Find(rpLayout.LayoutId);

            var RoutePageLayout = new RoutepageLayout
            {
                ID = Guid.NewGuid(),
                Category = rpLayout.Category,
                Layout = layout,
                LayoutID = rpLayout.LayoutId,
                Name = rpLayout.Name,
                Route = route
            };

            var rpZones = new List<Zone>();
            foreach (var zone in rpLayout.Zones)
            {
                var enumerable = zone.Widgets.Where(editModel => editModel.ID != Guid.Empty).Select(x => x.ID).ToList();
                var zoneGuid = Guid.NewGuid();
                var z = new Zone
                {
                    ID = zoneGuid,
                    Layout = RoutePageLayout,
                    LayoutID = rpLayout.LayoutId,
                    Name = zone.Name,
                    Widgets = new List<BaseWidget>()
                };

                var deletedWidgets = z.Widgets.Where(x => enumerable.Contains(x.ID) == false).ToList();
                var savedWidgets = z.Widgets.Where(x => enumerable.Contains(x.ID)).ToList();

                foreach (var deletedWidget in deletedWidgets)
                {
                    if (deletedWidget is GalleryWidget)
                    {
                        var ww = deletedWidget as GalleryWidget;
                        foreach (var fileGallery in ww.Files.ToList())
                        {
                            _db.Entry(fileGallery).State = EntityState.Deleted;
                        }

                        ww.Files.Clear();

                    }

                    _db.Entry(deletedWidget).State = EntityState.Deleted;
                }

                foreach (var editWidget in zone.Widgets)
                {

                    var updatingWidget = editWidget.ID != Guid.Empty
                        ? UpdateWidget(savedWidgets.Single(x => x.ID == editWidget.ID), editWidget)
                        : ParseWidget(editWidget);

                    updatingWidget.ZoneID = zoneGuid;
                    updatingWidget.Zone = z;

                    z.AddWidget(updatingWidget);
                }


                rpZones.Add(z);
            }

            RoutePageLayout.Zones = rpZones;

            _db.RoutePageLayouts.Add(RoutePageLayout);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Message("Обновлено")]
        public IHttpActionResult Put(Guid id, RoutePageLayoutsViewModel.Summary rpLayout)
        {
            var rpL = _db.RoutePageLayouts.Find(id);

            var rpZones = new List<Zone>();
            for (int i = 0; i < rpL.Zones.Count; i++)
            {
                var zone = rpL.Zones.ToList()[i];
                var updZone = rpLayout.Zones.ToList()[i];
                var enumerable = zone.Widgets.Where(editModel => editModel.ID != Guid.Empty).Select(x => x.ID).ToList();

                var z = _db.LandingPages.Find(zone.ID);

                var deletedWidgets = z.Widgets.Where(x => enumerable.Contains(x.ID) == false).ToList();
                var savedWidgets = z.Widgets.Where(x => enumerable.Contains(x.ID)).ToList();

                foreach (var deletedWidget in deletedWidgets)
                {
                    if (deletedWidget is GalleryWidget)
                    {
                        var ww = deletedWidget as GalleryWidget;
                        foreach (var fileGallery in ww.Files.ToList())
                        {
                            _db.Entry(fileGallery).State = EntityState.Deleted;
                        }

                        ww.Files.Clear();

                    }

                    _db.Entry(deletedWidget).State = EntityState.Deleted;
                }

                foreach (var editWidget in updZone.Widgets)
                {

                    var updatingWidget = editWidget.ID != Guid.Empty
                        ? UpdateWidget(savedWidgets.Single(x => x.ID == editWidget.ID), editWidget)
                        : ParseWidget(editWidget);

                    z.AddWidget(updatingWidget);
                }


                rpZones.Add(z);
            }

            rpL.Zones = rpZones;

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Message("Удалено")]
        public IHttpActionResult Delete(string shortcut, Guid rpId)
        {
            var rpLayout = _db.RoutePageLayouts.Find(rpId);
            var zones = _db.RoutePageLayouts.Find(rpId).Zones;
            var widgets = _db.RoutePageLayouts.Find(rpId).Zones.SelectMany(x => x.Widgets).ToList();

            foreach (var widget in widgets)
            {
                _db.Entry(widget).State = EntityState.Deleted;
            }

            foreach (var zone in zones.ToList())
            {
                _db.Entry(zone).State = EntityState.Deleted;
            }

            _db.Entry(rpLayout).State = EntityState.Deleted;

            _db.SaveChanges();

            var rpLayouts = _db.RoutePageLayouts.Where(x => x.Route.Shortcut.Equals(shortcut)).ToList();

            return Ok(rpLayouts.As<IList<RoutePageLayoutsViewModel.Details>>());
        }


        private BaseWidget ParseWidget(BaseWidgetViewModel.Details editWidget)
        {

            if (editWidget is GalleryWidgetViewModel.Details)
            {
                var gw = new GalleryWidget()
                {
                    ID = Guid.NewGuid(),
                    Files = new List<FileGallery>(),
                };

                var ids = ((GalleryWidgetViewModel.Details)editWidget).Files.Select(x => x.ID).ToList();
                var files = _db.FileEntries.Where(x => ids.Contains(x.ID)).ToList();

                foreach (var detail in files)
                {
                    gw.AddFile(detail);
                }
                return gw;
            }

            if (editWidget is DescriptionWidgetViewModel.Details)
            {
                var ew = editWidget as DescriptionWidgetViewModel.Details;
                return new DescriptionWidget()
                {
                    ID = Guid.NewGuid(),
                    Content = ew.Content
                };
            }

            if (editWidget is HeaderWidgetViewModel.Details)
            {
                var ew = editWidget as HeaderWidgetViewModel.Details;
                return new HeaderWidget()
                {
                    ID = Guid.NewGuid(),
                    Content = ew.Content
                };
            }

            if (editWidget is SplitWidgetViewModel.Details)
            {
                var ew = editWidget as SplitWidgetViewModel.Details;
                return new SplitWidget()
                {
                    ID = Guid.NewGuid(),
                    Left = ParseWidget(ew.Left),
                    Right = ParseWidget(ew.Right),
                };
            }

            if (editWidget is ContainerWidgetViewModel.Details)
            {
                var ew = editWidget as ContainerWidgetViewModel.Details;
                return new ContainerWidget()
                {
                    ID = Guid.NewGuid(),
                    Widget = ParseWidget(ew.Widget)
                };
            }

            if (editWidget is TextAdnDescriptionWidgetViewModel.Details)
            {
                var ew = editWidget as TextAdnDescriptionWidgetViewModel.Details;
                var photo = ew.Photo == null ? null : _db.Photos.Find(ew.Photo.ID);
                return new TextAdnDescriptionWidget()
                {
                    ID = Guid.NewGuid(),
                    Description = ew.Description,
                    Name = ew.Name,
                    Photo = photo,
                };
            }

            if (editWidget is RouteMenuWidgetViewModel.Details)
            {
                var ew = editWidget as RouteMenuWidgetViewModel.Details;

                return new MenuWidget()
                {
                    MenuID = Guid.NewGuid(),
                    Menu = _db.RouteMenus.Find(ew.Menu.ID),
                    ID = Guid.NewGuid()
                };
            }

            if (editWidget is CalendarsWidgetViewModel.Details)
            {
                var ew = editWidget as CalendarsWidgetViewModel.Details;

                foreach (var calendar in ew.CalendarWidgets)
                {
                    calendar.WidgetID = Guid.NewGuid();
                    calendar.ID = Guid.NewGuid();
                }

                var widgetsCollection = new List<CalendarWidget>();

                foreach (var widget in ew.CalendarWidgets)
                {
                    widgetsCollection.Add(new CalendarWidget()
                    {
                        ID = widget.ID,
                        Calendar = _db.Calendars.Find(widget.CalendarID),
                        CalendarID = widget.CalendarID,
                        WidgetID = widget.WidgetID,
                        Widget = _db.Widgets.Find(widget.WidgetID).As<CalendarsWidget>()
                    });
                }

                return new CalendarsWidget()
                {
                    ID = Guid.NewGuid(),
                    CalendarWidgets = widgetsCollection
                };
            }

            if (editWidget is HallWidgetViewModel.Details)
            {
                var ew = editWidget as HallWidgetViewModel.Details;
                var hall = new HallWidget()
                {
                    ID = Guid.NewGuid(),
                    Hall = _db.Halls.Find(ew.Hall.ID),
                    HallID = ew.Hall.ID
                };
                return hall;
            }

            throw new NotImplementedException();
        }

        private BaseWidget UpdateWidget(BaseWidget single, BaseWidgetViewModel.Details editWidget)
        {
            if (editWidget is GalleryWidgetViewModel.Details)
            {
                var gw = single as GalleryWidget;

                foreach (var fileGallery in gw.Files.ToList())
                {
                    _db.Entry(fileGallery).State = EntityState.Deleted;
                }

                gw.Files.Clear();

                var ids = ((GalleryWidgetViewModel.Details)editWidget).Files.Select(x => x.ID).ToList();
                var data = _db.FileEntries.ToList();
                var files = data.Where(x => ids.Contains(x.ID)).ToList();


                foreach (var detail in files)
                {
                    gw.AddFile(detail);
                }
                return gw;
            }

            if (editWidget is DescriptionWidgetViewModel.Details)
            {
                var ew = editWidget as DescriptionWidgetViewModel.Details;
                var sw = single as DescriptionWidget;
                sw.Content = ew.Content;
                return sw;
            }

            if (editWidget is HeaderWidgetViewModel.Details)
            {
                var ew = editWidget as HeaderWidgetViewModel.Details;
                var sw = single as HeaderWidget;
                sw.Content = ew.Content;
                return sw;
            }

            if (editWidget is SplitWidgetViewModel.Details)
            {
                var ew = editWidget as SplitWidgetViewModel.Details;
                var sp = single as SplitWidget;
                sp.Left = UpdateWidget(sp.Left, ew.Left);
                sp.Right = UpdateWidget(sp.Right, ew.Right);
            }

            if (editWidget is ContainerWidgetViewModel.Details)
            {
                var ew = editWidget as ContainerWidgetViewModel.Details;
                var sw = single as ContainerWidget;
                sw.Widget = UpdateWidget(sw.Widget, ew.Widget);
                return sw;
            }

            if (editWidget is TextAdnDescriptionWidgetViewModel.Details)
            {
                var ew = editWidget as TextAdnDescriptionWidgetViewModel.Details;
                var sw = single as TextAdnDescriptionWidget;
                var photo = ew.Photo == null ? null : _db.Photos.Find(ew.Photo.ID);
                sw.Description = ew.Description;
                sw.Name = ew.Name;
                sw.Photo = photo;
                return sw;
            }


            if (editWidget is CalendarsWidgetViewModel.Details)
            {
                var ew = editWidget as CalendarsWidgetViewModel.Details;
                var sw = single as CalendarsWidget;

                var widgetsCollection = new List<CalendarWidget>();

                foreach (var calendarWidget in sw.CalendarWidgets.ToList())
                {
                    _db.Entry(calendarWidget).State = EntityState.Deleted;
                }
                sw.CalendarWidgets.Clear();

                foreach (var widget in ew.CalendarWidgets)
                {
                    widgetsCollection.Add(new CalendarWidget()
                    {
                        ID = widget.ID,
                        Calendar = _db.Calendars.Find(widget.CalendarID),
                        CalendarID = widget.CalendarID,
                        WidgetID = widget.WidgetID,
                        Widget = _db.Widgets.Find(widget.WidgetID).As<CalendarsWidget>()
                    });
                }


                sw.CalendarWidgets = widgetsCollection;
                return sw;
            }

            if (editWidget is RouteMenuWidgetViewModel.Details)
            {
                var ew = editWidget as RouteMenuWidgetViewModel.Details;
                var sw = single as MenuWidget;

                sw.Menu.Name = ew.Menu.Name;
                sw.Menu.Publish = ew.Menu.Publish;
                sw.Menu.Order = ew.Menu.Order;

                return sw;
            }

            if (editWidget is HallWidgetViewModel.Details)
            {
                var ew = editWidget as HallWidgetViewModel.Details;
                var sw = single as HallWidget;

                sw.Hall = _db.Halls.Find(ew.Hall.ID);
                sw.HallID = ew.Hall.ID;
                return sw;
            }
            throw new NotImplementedException();
        }
    }
}
