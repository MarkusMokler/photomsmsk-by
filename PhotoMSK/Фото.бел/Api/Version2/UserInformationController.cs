using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using Fotobel.Classes;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;
using PhoneNumbers;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Routes;
using PhotoMSK.ViewModels.Routes.Photostudio;

namespace Fotobel.Api.Version2
{
    public class UserInformationController : ApiController
    {
        readonly AppContext _context = new AppContext();
        /// <summary>
        /// Get user events
        /// </summary>
        /// <returns>IList<EventsViewModel></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            var name = User.Identity.Name;
            var data = _context.UserInformations.Single(x=>x.User.UserName == name).Events.Where(x => x.Start >= DateTime.Now).OrderBy(x=>x.Start).ToList().As<IList<EventViewModel>>();
            var dataRet = new List<Object>();
            for (int i = 0; i < data.Count; i++)
            {
                Calendar routeGet = _context.Calendars.Find(data[i].CalendarID);
                
                if (routeGet!=null)
                dataRet.Add(new
                {
                    routeName = routeGet.RouteEntity.GetName(),
                    routeShortcut = routeGet.RouteEntity.Shortcut,
                    bookItem = routeGet.GetName(),
                    start = data[i].Start,
                    end = data[i].End,
                    price = data[i].Price
                });
            }
            
            return Ok(new { data = dataRet});

        }
        
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_context.UserInformations.Find(id).As<UserInformationViewModel.Summary>());

        }
        [HttpGet]
        public IHttpActionResult FindByPhone(string search)
        {
            var strName = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = "";
            var lastName = "";
            if (strName.Length > 1)
            {
                firstName = strName[0];
                lastName = strName[1];
                return (FindByName(firstName, lastName));
            }

            var str = search;
            if (search.StartsWith("+") == false)
                str = "+" + search;
                
            var phones = _context.Phones.Where(x => x.Number.StartsWith(str))
                .OrderBy(x => x.Number)
                .Select(x => x.UserPhone.User).Take(10).ToList().Where(x => x != null).ToList();
            return Ok(Mapper.Map<IList<UserInformationViewModel.Details>>(phones));
        }

        public IHttpActionResult FindByName(string firstName, string lastName)
        {
            var phones = _context.Phones.Where(x => x.UserPhone.User.FirstName.Contains(firstName) &&
            x.UserPhone.User.LastName.Contains(lastName) ||
            x.UserPhone.User.FirstName.Contains(lastName) &&
            x.UserPhone.User.LastName.Contains(firstName))
                .OrderBy(x => x.Number)
                .Select(x => x.UserPhone.User).Take(10).ToList().Where(x => x != null).ToList();
            return Ok(Mapper.Map<IList<UserInformationViewModel.Details>>(phones));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(UserInformationViewModel.Summary user)
        {
            if (user.UserPhone == null)
                return BadRequest("Введите номер телефона");
            var userphone = user.UserPhone.Replace("%2B","+").Replace("-", "").Replace(" ", "");

            var id = Guid.NewGuid();

            var info = new PhotoMSK.Data.Models.UserInformation
            {
                ID = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phones = new HashSet<UserPhone>
                    {
                        new UserPhone
                        {
                            ID = id,
                            Confirm = false,
                            DateAdded = DateTime.Now,
                            Phone = new Phone
                            {
                                ID = id,
                                Number = userphone
                            }
                        }
                    }
            };
            _context.UserInformations.Add(info);
            _context.SaveChanges();
            return Ok(_context.UserInformations.Find(id).As<UserInformationViewModel.Summary>());
        }

        [HttpPut]
        [Message("Профиль обновлен")]
        public IHttpActionResult Put(Guid id, Proxy<PhotoMSK.Data.Models.UserInformation> information)
        {
            var userInformation = _context.UserInformations.Find(id);
            information.Patch(userInformation);
            _context.SaveChanges();
            return Ok(userInformation.As<UserInformationViewModel.Summary>());

        }

        [HttpGet]
        public IHttpActionResult GetUserPhones(Guid userGuid)
        {
            var phones = _context.Phones.Where(x => x.UserPhone.UserID == userGuid).As<IList<PhoneViewModel.Summary>>();
            return Ok(phones);
        }

        public IHttpActionResult Get(string phone)
        {
            var info = _context.Phones.SingleOrDefault(x => x.Number.StartsWith(phone));

            if (info != null)
                return Ok(info.UserPhone.User);
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}