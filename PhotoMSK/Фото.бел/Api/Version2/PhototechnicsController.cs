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
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;


namespace Fotobel.Api.Version2
{
    public class PhototechnicsController : ApiController
    {
        readonly IPhototechnicsService _phototechnicsService;

        private readonly ILuceneProvider _provider;
        private readonly AppContext _context = new AppContext();
        public PhototechnicsController(IPhototechnicsService phototechnicsService)
        {
            _phototechnicsService = phototechnicsService;

        }

        public IHttpActionResult Get(int page = 0, int pageSize = 20)
        {
            var data = _phototechnicsService.WhereSummary(new PageRequest<Phototechnics>()
            {
                Page = page,
                Size = pageSize,

            });

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult Get(string search)
        {

            //var q = _provider.ParseQuery(new[] { "Name" }, string.Format("Name:*{0}*", search.Replace(" ", "*").ToLower()));


            //TopDocs results = _provider.Searcher.Search(q, 20);

            //var guids = results.ScoreDocs.Select(x => new Guid(_provider.Searcher.Doc(x.Doc).Get("Id")));

            return Ok(_phototechnicsService.WhereSummary(new PageRequest<Phototechnics>() { Where = x => x.Name.Contains(search) }));

        }

        [HttpGet]
        public IHttpActionResult GetShortcut(string id)
        {
            return Ok(_phototechnicsService.Where(new PageRequest<Phototechnics>() { Where = x => x.Shortcut == id }).Elements.First());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
