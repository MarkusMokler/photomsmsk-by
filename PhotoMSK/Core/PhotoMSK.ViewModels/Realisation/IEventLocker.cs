using System;
using System.Collections.Generic;
using PhotoMSK.ViewModels.Routes.Photostudio;

namespace PhotoMSK.ViewModels.Realisation
{
    public interface IEventLocker
    {
        bool Lock(Guid calendarID, DateTime start, DateTime end, Guid uid);
        IEnumerable<EventViewModel> GetLockers(Guid calendarID);
        void ClearLocks();
        void ClearLocks(Guid userID);
    }
}