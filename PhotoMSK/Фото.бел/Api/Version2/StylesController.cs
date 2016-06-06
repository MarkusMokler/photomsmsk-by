using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.RoutePageLayouts;
using Fotobel.Classes;
namespace Fotobel.Api.Version2
{
    public class StylesController : ApiController
    {
        AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            var styles = _context.ThemeStyleses.Where(x => x.Route.Shortcut.Equals(shortcut));
            return Ok(styles.As<List<ThemeStyleViewModel>>());
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            var style = _context.ThemeStyleses.Find(id);
            return Ok(style.As<ThemeStyleViewModel>());
        }

        [HttpPost]
        [Message("Стиль сохранен")]
        public IHttpActionResult Post(ThemeStyles style)
        {
            style.ID = Guid.NewGuid();
            
            if (style.RouteID != Guid.Empty)
            {
                style.Route = _context.Routes.Find(style.RouteID);
            }
            _context.ThemeStyleses.Add(style);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Message("Стиль обновлен")]
        public IHttpActionResult Put(ThemeStyles style)
        {
            var styleEt = _context.ThemeStyleses.Find(style.ID);
            styleEt.Name = style.Name;
            styleEt.Style = style.Style;
            if (style.RouteID != Guid.Empty)
            {
                var route = _context.Routes.Find(style.RouteID);
                styleEt.Route = route;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
