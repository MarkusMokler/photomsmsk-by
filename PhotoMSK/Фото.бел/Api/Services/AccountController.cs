using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Fotobel.Classes;
using Google.Apis.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Permission;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;
using PhotoMSK.ViewModels.Realisation;


namespace Fotobel.Api.Services
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly AppContext _context;
        private ISmsAssistent _assistent;

        public AccountController(ISmsAssistent assistent)
        {
            _assistent = assistent;
            _context = new AppContext();
            UserManager = new UserManager<User>(new UserStore<User>(_context));
        }

        public UserManager<User> UserManager { get; private set; }

        [HttpGet]
        public IHttpActionResult CheckUserAccess(Guid id, string shortcut)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            if (!route.IsActionPermited(EventManage.CreateAdminEvent, id))
                throw new ValidationException("У вас нет прав для доступа");
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetUserInformation()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ui = _context.UserInformations.Single(x => x.User.UserName == User.Identity.Name);
                return Ok(new
                {
                    IsAuthenticated = true,
                    UserInformation = ui.As<UserInformationViewModel.Summary>()
                });
            }
            return Ok(new { IsAuthenticated = false });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            User user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user == null)
            {
                var username =
                    _context.Phones.Where(x => x.Number == model.UserName && x.UserPhone != null && x.UserPhone.Confirm)
                        .Select(x => x.UserPhone.User.User.UserName).FirstOrDefault();
                if (username != null)
                    user = await UserManager.FindAsync(username, model.Password);
            }
            if (user == null)
                throw new ValidationException("Пользователь с таким никнеймом или телефоном не найден.");

            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);


            AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = DateTimeOffset.Now;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));


            return Ok(model.UserName);
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Create(string id)
        {
            Int64 buff;
            string number = id.Replace("-", "").Replace(" ", "");

            if (number.StartsWith("80"))
            {
                var regex = new Regex(Regex.Escape("80"));
                number = regex.Replace(number, "+375", 1);
            }
            if (number.StartsWith("+") == false)
            {
                number = "+" + number;
            }

            var reg = new Regex(Regex.Escape("+"));
            var validCheck = reg.Replace(number, "", 1);
            if (!Int64.TryParse(validCheck, out buff))
            {
                throw new ValidationException("Введенный номер не верного формата");
            }
            Phone phone = _context.Phones.SingleOrDefault(x => x.Number == number);

            int code = new Random().Next(0, 10000);


            if (phone == null)
            {
                Guid uid = Guid.NewGuid();

                _context.Phones.Add(new Phone
                {
                    ID = Guid.NewGuid(),
                    Number = number,
                    DateLastSending = DateTime.Now,
                    UserPhone =
                        new UserPhone
                        {
                            ID = Guid.NewGuid(),
                            Confirm = false,
                            ConfirmCode = code,
                            DateAdded = DateTime.Now,
                            User = new UserInformation
                            {
                                ID = uid
                            }
                        }
                });


                _assistent.SendMessage(number, $"Ваш проверочный код {code}");
                _context.SaveChanges();
                return Ok(new { action = "edit", UserID = uid });
            }

            Guid idd;
            if (phone.UserPhone == null)
            {
                idd = Guid.NewGuid();
                phone.UserPhone =
                    new UserPhone
                    {
                        ID = Guid.NewGuid(),
                        Confirm = false,
                        ConfirmCode = code,
                        DateAdded = DateTime.Now,
                        User = new UserInformation
                        {
                            ID = idd
                        }
                    };
            }
            else
            {
                idd = phone.UserPhone.User.ID;
            }


            int next = new Random().Next(0, 10000);

            phone.UserPhone.ConfirmCode = next;

            if (phone.DateLastSending >= DateTime.Now.AddMinutes(-2))
            {
                throw new ValidationException("Вам уже отправлен код подтверждения. Через две минуты Вы сможете повторить запрос.");
            }
            _assistent.SendMessage(id, $"Ваш проверочный код {next}");
            phone.DateLastSending = DateTime.Now;
            _context.SaveChanges();
            if (phone.UserPhone.Confirm)
            {
                return Ok(new { action = "reset", UserID = idd });
            }

            return Ok(new { action = "edit", UserID = idd });
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Register(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                UserPhone phone =
                    _context.UserPhones.SingleOrDefault(x => x.UserID == model.UserID && x.ConfirmCode == model.ConfirmCode);

                if (phone == null)
                {
                    throw new ValidationException("Не верный код подтверждения.");
                }


                if (_context.Users.Any(x => x.UserName == model.UserName))
                {
                    throw new ValidationException("Такой пользователь уже есть.");
                }
                if (model.Password.Length < 6)
                {
                    throw new ValidationException("Пароль должен состоять минимум из 6-ти символов.");
                }
                phone.Confirm = true;

                UserInformation entity = phone.User;

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;

                var user = new User { UserName = model.UserName, UserInformation = entity };
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    //    SignIn(user, true);

                    _context.SaveChanges();

                    return Ok();
                }
                foreach (var error in result.Errors)
                {
                    throw new ValidationException(error);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Reset(ResetModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserPhone phone =
                _context.UserPhones.SingleOrDefault(x => x.UserID == model.UserID && x.ConfirmCode == model.ConfirmCode);

            if (phone == null)
            {
                throw new ValidationException("Не верный код подтверждения");
            }

            phone.Confirm = true;

            UserInformation entity = phone.User;

            UserManager.RemovePassword(entity.User.Id);

            IdentityResult result = UserManager.AddPassword(entity.User.Id, model.Password);

            //      await SignInAsync(entity.User, true);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Confirm(int confirmCode)
        {
            UserPhone result = _context.Users.Single(x => x.UserName == User.Identity.Name)
                .UserInformation.Phones.FirstOrDefault(x => x.ConfirmCode == confirmCode);
            result.Confirm = true;
            if (string.IsNullOrEmpty(result.Phone.Number))
            {
                result.User.User.PhoneNumber = result.Phone.Number;
                result.User.User.PhoneNumberConfirmed = true;
            }
            _context.SaveChanges();
            return Ok(true);
        }

        public IHttpActionResult LogOff()
        {
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
    }

    public class CheckModel
    {
        public int Code { get; set; }
    }

    public class UserEditModel
    {
        public Guid UserID { get; set; }
        public int ConfirmCode { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

    }

    public class ResetModel
    {
        public Guid UserID { get; set; }
        public int ConfirmCode { get; set; }
        public string Password { get; set; }

    }


}
