using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Fotobel.Classes;
using Lucene.Net.Search;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;

namespace Fotobel.Api.Version2
{
    public class SearchController : ApiController
    {
        private readonly ILuceneProvider _provider;
        private readonly IPhototechnicsService _phototechnicsService;
        private readonly AppContext _context = new AppContext();
        public SearchController(ILuceneProvider provider, IPhototechnicsService phototechnicsService)
        {
            _provider = provider;
            _phototechnicsService = phototechnicsService;
        }

        [HttpGet]
        public IList<PhototechnicsViewModel.Summary> Get(string search, Guid routeId)
        {

            var q = _provider.ParseQuery(new[] { "Name" }, string.Format("Name:*{0}*", search.Replace(" ", "*").ToLower()));

            var results = _provider.Searcher.Search(q, 400);

            var guids = results.ScoreDocs.Select(x => new Guid(_provider.Searcher.Doc(x.Doc).Get("ID")));

            var res = _context.Routes.Find(routeId);

            if (res is PhotoMSK.Data.Models.Routes.Photoshop)
                return
                    _phototechnicsService.GetPhotoshopTechnics(res.As<PhotoshopViewModel.Summary>(),
                        new PageRequest<PricePosition>() { Where = x => guids.Contains(x.PhototechnicsID) }).Elements;
            else
            {
                return _phototechnicsService.GetPhotorentTechnics(res.As<PhotorentViewModel.Summary>(),
                        new PageRequest<RentCalendar>() { Where = x => guids.Contains(x.PhototechnicsID) }).Elements;

            }
        }

        [HttpGet]
        public IHttpActionResult GetRoutes(string searchRoute)
        {

            var rres = _context.Routes.Where(x => x.Shortcut.Contains(searchRoute)).ToList().Select(x => new
            {
                x.ID,
                x.ImageUrl,
                Name = x.GetName(),
                x.Shortcut,
                Etype = x.GetType().Name.Split('_').First()
            }).ToList();
            return Ok(rres);
        }

        [HttpGet]
        public HttpResponseMessage Get(string search)
        {

            var q = _provider.ParseQuery(new[] { "Name" }, string.Format("Name:*{0}*", search.Replace(" ", "*").ToLower()));


            TopDocs results = _provider.Searcher.Search(q, 20);

            var guids = results.ScoreDocs.Select(x => new Guid(_provider.Searcher.Doc(x.Doc).Get("ID")));

            var rres = _context.Routes.Where(x => guids.Contains(x.ID)).ToList().Select(x => new
            {
                x.ID,
                x.ImageUrl,
                Name = x.GetName(),
                x.Shortcut,
                Etype = x.GetType().Name.Split('_').First()
            }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, rres);
        }

        [HttpPost]
        public IHttpActionResult FindByPhone([FromBody] SearchRequest rq)
        {
            var str = rq.Search.Replace("-", "");
            var phones = _context.Phones.Where(x => x.Number.StartsWith(str)).OrderBy(x => x.Number).Select(x=>x.UserPhone.User).Take(10);
            return Ok(Mapper.Map<IList<UserInformationViewModel.Details>>(phones.ToList()));
        }

        [HttpGet]
        public IHttpActionResult Rebuild()
        {

            var writer = _provider.Writer;
            writer.DeleteAll();
            writer.Commit();
            var routes = _context.Routes.ToList();

            foreach (var routeEntity in routes)
            {

                writer.Write(routeEntity);
            }

            writer.Commit();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }

    public class SearchRequest
    {
        public string Search { get; set; }

    }
}
