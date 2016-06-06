using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper.Internal;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Widgets;
using FileGallery = PhotoMSK.Data.Models.Widgets.FileGallery;

namespace Fotobel.Api.Version2
{
    public class LandingController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_context.LandingPages.Find(id).As<LandingPageViewModel.Details>());
        }

        [HttpPost]
        public IHttpActionResult Post(LandingPageViewModel.Details model)
        {
            var enumerable = model.Widgets.Where(editModel => editModel.ID != Guid.Empty).Select(x => x.ID).ToList();

            var landing = new Zone()
            {
                ID = Guid.NewGuid(),
                Widgets = new List<BaseWidget>()
            };

            var deletedWidgets = landing.Widgets.Where(x => enumerable.Contains(x.ID) == false).ToList();
            var savedWidgets = landing.Widgets.Where(x => enumerable.Contains(x.ID)).ToList();

            foreach (var deletedWidget in deletedWidgets)
            {
                if (deletedWidget is GalleryWidget)
                {
                    var ww = deletedWidget as GalleryWidget;
                    foreach (var fileGallery in ww.Files.ToList())
                    {
                        _context.Entry(fileGallery).State = EntityState.Deleted;
                    }

                    ww.Files.Clear();

                }

                _context.Entry(deletedWidget).State = EntityState.Deleted;
            }


            foreach (var editWidget in model.Widgets)
            {

                var updatingWidget = editWidget.ID != Guid.Empty
                    ? UpdateWidget(savedWidgets.Single(x => x.ID == editWidget.ID), editWidget)
                    : ParseWidget(editWidget);

                landing.AddWidget(updatingWidget);
            }

            _context.LandingPages.Add(landing);
            _context.SaveChanges();

            return Ok(landing.As<LandingPageViewModel.Details>());
        }


        [HttpPut]
        public IHttpActionResult Put(Guid id, LandingPageViewModel.Details model)
        {
            var enumerable = model.Widgets.Where(editModel => editModel.ID != Guid.Empty).Select(x => x.ID).ToList();

            var landing = _context.LandingPages.Find(id);

            var deletedWidgets = landing.Widgets.Where(x => enumerable.Contains(x.ID) == false).ToList();
            var savedWidgets = landing.Widgets.Where(x => enumerable.Contains(x.ID)).ToList();

            foreach (var deletedWidget in deletedWidgets)
            {
                if (deletedWidget is GalleryWidget)
                {
                    var ww = deletedWidget as GalleryWidget;
                    foreach (var fileGallery in ww.Files.ToList())
                    {
                        _context.Entry(fileGallery).State = EntityState.Deleted;
                    }

                    ww.Files.Clear();

                }
                _context.Entry(deletedWidget).State = EntityState.Deleted;
            }

            landing.Widgets = new List<BaseWidget>();

            foreach (var editWidget in model.Widgets)
            {

                var updatingWidget = editWidget.ID != Guid.Empty
                    ? UpdateWidget(savedWidgets.Single(x => x.ID == editWidget.ID), editWidget)
                    : ParseWidget(editWidget);

                landing.AddWidget(updatingWidget);
            }

            _context.SaveChanges();
            return Ok(landing.As<LandingPageViewModel.Details>());
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
                var files = _context.FileEntries.Where(x => ids.Contains(x.ID)).ToList();

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
                var photo = ew.Photo == null ? null : _context.Photos.Find(ew.Photo.ID);
                return new TextAdnDescriptionWidget()
                {
                    ID = Guid.NewGuid(),
                    Description = ew.Description,
                    Name = ew.Name,
                    Photo = photo,
                };
            }

            throw new NotImplementedException();
        }


        //case "faqWidget":
        //    {
        //        return new FaqWidgetViewModel.Details();
        //    }



        private BaseWidget UpdateWidget(BaseWidget single, BaseWidgetViewModel.Details editWidget)
        {
            if (editWidget is GalleryWidgetViewModel.Details)
            {
                var gw = single as GalleryWidget;

                foreach (var fileGallery in gw.Files.ToList())
                {
                    _context.Entry(fileGallery).State = EntityState.Deleted;
                }

                gw.Files.Clear();

                var ids = ((GalleryWidgetViewModel.Details)editWidget).Files.Select(x => x.ID).ToList();
                var data = _context.FileEntries.ToList();
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
                var photo = ew.Photo == null ? null : _context.Photos.Find(ew.Photo.ID);
                sw.Description = ew.Description;
                sw.Name = ew.Name;
                sw.Photo = photo;
                return sw;
            }

            throw new NotImplementedException();
        }
    }
}
