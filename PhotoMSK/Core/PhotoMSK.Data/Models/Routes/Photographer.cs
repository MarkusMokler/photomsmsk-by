using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data.Models.Attachments;

namespace PhotoMSK.Data.Models.Routes
{
    public class Photographer : RouteEntity
    {
        public Photographer()
        {
            _shotings = new Lazy<IList<Shooting>>(() => { return Wall.Select(x => x.WallPost).OfType<Shooting>().ToList(); });

        }
        private Lazy<IList<Shooting>> _shotings;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IEnumerable<Shooting> Shootings => _shotings.Value;
        public virtual ICollection<Creative> Creatives { get; set; }
        public virtual PhotographerCalendar Calendar { get; set; }
        public virtual ICollection<PhotoGener> Geners { get; set; }

        public override string ToString()
        {
            return LastName + " " + FirstName;
        }
        public void AddGenre(Genre gr)
        {
            if (Geners == null)
                Geners = new List<PhotoGener>();

            PhotoGener gener = Geners.SingleOrDefault(x => x.Genre.Name == gr.Name);
            if (gener == null)
                Geners.Add(new PhotoGener { ID = Guid.NewGuid(), Genre = gr, Level = 0 });
        }
        public override string GetName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}