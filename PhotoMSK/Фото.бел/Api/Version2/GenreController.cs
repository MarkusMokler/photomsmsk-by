using System.Linq;
using System.Web.Http;
using PhotoMSK.Data;

namespace Fotobel.Api.Version2
{
    public class GenreController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpPost]
        public IHttpActionResult FindGenres([FromBody] SearchRequest search)
        {
            return Ok(_context.Genres.Where(x => x.Name.StartsWith(search.Search)).Select(x => x.Name).Take(10).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
