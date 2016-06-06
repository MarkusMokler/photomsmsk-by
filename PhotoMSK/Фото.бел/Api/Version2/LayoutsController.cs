using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Version2
{
    [Authorize]
    public class LayoutsController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            return Ok(_context.Layouts.Take(10).ToList());
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            return Ok(_context.Layouts.Find(id));
        }

        [HttpPost]
        [Message("Добавлено")]
        public IHttpActionResult Post(string shortcut, Layout layout)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            layout.ID = Guid.NewGuid();
            _context.Layouts.Add(layout);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Message("Обновлено")]
        public IHttpActionResult Put(Guid id, Layout layout)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var layoutEdit = _context.Layouts.Find(id);
            layoutEdit.Content = layout.Content;
            layoutEdit.Name = layout.Name;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Message("Шаблон удален")]
        public IHttpActionResult Delete(Guid id)
        {
            var layout = _context.Layouts.Find(id);
            _context.Layouts.Remove(layout);
            _context.SaveChanges();
            return Ok(_context.Layouts.Take(10).ToList());
        }
    }
}
