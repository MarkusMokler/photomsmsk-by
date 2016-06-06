using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace Fotobel.Api.Services
{
    public class MergeController : ApiController
    {
        AppContext _context = new AppContext();
        [HttpGet]
        public IHttpActionResult MergePhone()
        {
            var list = _context.Phones.GroupBy(x => x.Number).Where(x => x.Count() > 1).ToList();
            foreach (var key in list.Select(x => x.Key))
            {
                var phones = _context.Phones.Where(x => x.Number == key).ToList();

                phones.Sort(new PhoneComparer());

                var first = phones.First();

                var pphones = phones.Skip(1);

                foreach (var pphone in pphones)
                {
                    foreach (var rp in pphone.RoutesPhone)
                    {
                        rp.Phone = first;
                        rp.PhoneID = first.ID;
                    }
                    if (pphone.UserPhone != null && pphone.UserPhone.User != null)
                    {
                        foreach (var @event in pphone.UserPhone.User.Events)
                        {
                            @event.User = first.UserPhone.User;
                        }

                        foreach (var @event in pphone.UserPhone.User.Penalties)
                        {
                            @event.User = first.UserPhone.User;
                        }

                        foreach (var @event in pphone.UserPhone.User.Cards)
                        {
                            @event.UserInformation = first.UserPhone.User;
                        }

                        _context.Entry(pphone.UserPhone.User).State = EntityState.Deleted;

                        _context.Entry(pphone.UserPhone).State = EntityState.Deleted;
                    }
                    _context.Entry(pphone).State = EntityState.Deleted;
                }
            }
            _context.SaveChanges();

            return Ok();
        }


        public class PhoneComparer : IComparer<Phone>
        {
            public int Compare(Phone x, Phone y)
            {
                if (x.UserPhone == null && y.UserPhone == null)
                    return 0;

                if (x.UserPhone != null && y.UserPhone == null)
                    return -1;

                if (x.UserPhone == null && y.UserPhone != null)
                    return 1;

                if (x.UserPhone.User != null && y.UserPhone.User == null)
                    return -1;

                if (x.UserPhone.User == null && y.UserPhone.User != null)
                    return 1;

                return 0;

            }
        }
    }
}
