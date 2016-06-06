using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Migrations;
using PhotoMSK.ViewModels.Routes.Photostudio;

namespace PhotoMSK.ViewModels.Realisation
{
    class EventLock
    {
        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly Guid _uid;
        private readonly DateTime _created;
        public DateTime Start => _start;
        public DateTime End => _end;

        public Guid UserId => _uid;

        public EventLock(DateTime start, DateTime end, Guid uid)
        {
            _end = end;
            _uid = uid;
            _start = start;
            _created = DateTime.Now;
        }

        public bool IsInInterval(DateTime start, DateTime end)
        {
            return (start >= _start && start < _end) || (end > _start && end <= _end) || (start <= _start && end >= _end);
        }
        public bool IsExpired => DateTime.Now.AddMinutes(-10) > _created;

    }

    class EventLocker : IEventLocker
    {
        static readonly object Locker = new object();
        static readonly Dictionary<Guid, List<EventLock>> Dictionary = new Dictionary<Guid, List<EventLock>>();

        public bool Lock(Guid calendarID, DateTime start, DateTime end, Guid uid)
        {
            lock (Locker)
            {
                if (IsLocked(calendarID, start, end))
                    return false;

                if (!Dictionary.ContainsKey(calendarID))
                    Dictionary.Add(calendarID, new List<EventLock>());
                Dictionary[calendarID].Add(new EventLock(start, end, uid));
                return true;
            }
        }
        public IEnumerable<EventViewModel> GetLockers(Guid calendarID)
        {
            try
            {
                lock (Locker)
                {
                    return Dictionary[calendarID].Where(x => x.IsExpired == false).Select(x => new EventViewModel()
                    {
                        Start = x.Start,
                        End = x.End,
                        Title = "сейчас бронируется"
                    });
                }
            }
            catch (Exception)
            {
                return new List<EventViewModel>();
            }
        }
        public void ClearLocks()
        {
            lock (Locker)
            {
                foreach (var value in Dictionary.Values.ToList())
                {
                    foreach (var result in value.Where(x => x.IsExpired).ToList() )
                    {
                        value.Remove(result);
                    }

                }
            }
        }

        public void ClearLocks(Guid userID)
        {
            lock (Locker)
            {
                foreach (var value in Dictionary.Values.ToList())
                {
                    foreach (var result in value.Where(x => x.UserId == userID).ToList())
                    {
                        value.Remove(result);
                    }

                }
            }
        }

        private static bool IsLocked(Guid calendarID, DateTime start, DateTime end)
        {
            try
            {
                return Dictionary[calendarID].Any(x => x.IsInInterval(start, end));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
