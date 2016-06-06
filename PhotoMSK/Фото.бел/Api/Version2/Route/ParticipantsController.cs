using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Permission;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class ParticipantController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut, int page = 0, int size = 20, string search = null)
        {
            if (string.IsNullOrEmpty(search))
                return Ok(new PageView<UserInformationViewModel.Participant>
                {
                    Elements =
                        _context.Participants.Where(x => x.Route.Shortcut == shortcut)
                            .Include(x => x.UserInformation)
                            .ToList()
                            .As<IList<UserInformationViewModel.Participant>>(),
                    ItemsCount = _context.Participants.Count(x => x.Route.Shortcut == shortcut),
                    PageSize = size
                });
            var str = search;
            if (search.StartsWith("+") == false)
            {
                str = "+" + search;
                str = str.Replace(" ", string.Empty);
            }

            var data = new PageView<UserInformationViewModel.Participant>
            {
                Elements = _context.Phones.Where(x => x.Number.Contains(str))
                    .OrderBy(x => x.Number)
                    .Select(x => x.UserPhone.User)
                    .Where(x => x != null)
                    .Skip(page * size)
                    .Take(size)
                    .ToList()
                    .As<IList<UserInformationViewModel.Participant>>(),
                ItemsCount =
                    _context.Phones.Where(x => x.Number.Contains(str))
                        .Select(x => x.UserPhone.User)
                        .Count(x => x != null),
                PageSize = size
            };
            return Ok(data);
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, UserInformationViewModel.Participant model)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var adminUid = User.GetUserInformationID();

            if (
                !route.IsActionPermited(AccountManage.AddAdministrator, adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);

            var uinfo = _context.Phones
            .Where(x => x.Number.StartsWith(model.UserPhone)).Select(x => x.UserPhone.User).Single(x => x != null);

            if (route.Participants.Any(z => z.UserInformation.ID == uinfo.ID))
                throw new ValidationException("Пользователь уже есть среди администраторов", MessageCodes.OkAction);

            route.Participants.Add(new Role()
            {
                ID = Guid.NewGuid(),
                AccessStatus = model.AccessStatus,
                UserInformation = uinfo
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(string shortcut, Guid id, UserInformationViewModel.Participant model)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var adminUid = User.GetUserInformationID();

            if (
                !route.IsActionPermited(AccountManage.RemoveAdministrator, adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);

            _context.Participants.Find(id).AccessStatus = model.AccessStatus;
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(string shortcut, Guid id)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var adminUid = User.GetUserInformationID();

            if (!route.IsActionPermited(AccountManage.RemoveAdministrator, adminUid))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);


            var ee = _context.Participants.Find(id);
            _context.Entry(ee).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }



    }
}
