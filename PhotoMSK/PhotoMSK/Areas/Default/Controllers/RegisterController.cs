using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PhotoMSK.Areas.Default.Models;
using PhotoMSK.Areas.Default.ViewData.Register;
using PhotoMSK.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppContext _db = new AppContext();
        private readonly UserManager<User> _userManager;

        public RegisterController()
        {
            _userManager = new UserManager<User>(new UserStore<User>(_db)) { SmsService = new SmsAssistent() };
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        // GET: /Register/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string id)
        {
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


            Phone phone = _db.Phones.SingleOrDefault(x => x.Number == number);
            int code = new Random().Next(0, 10000);
            var assistent = new SmsAssistent();

            if (phone == null)
            {
                Guid uid = Guid.NewGuid();

                _db.Phones.Add(new Phone
                {
                    ID = Guid.NewGuid(),
                    Number = number,
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

                _db.SaveChanges();

                assistent.SendMessage(number, string.Format("Ваш проверочный код {0}", code));

                return RedirectToAction("Edit", "Register", new { id = uid });
            }

            Guid idd = phone.UserPhone.User.ID;
            int next = new Random().Next(0, 10000);

            phone.UserPhone.ConfirmCode = next;

            _db.SaveChanges();
            assistent.SendMessage(id, string.Format("Ваш проверочный код {0}", next));

            if (phone.UserPhone.Confirm)
            {
                return RedirectToAction("Reset", "Register", new { id = idd });
            }

            return RedirectToAction("Edit", "Register", new { id = idd });
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserInformation userinformation = _db.UserInformations.Find(id);

            if (userinformation == null)
                return HttpNotFound();

            var model = new EditViewData()
            {
                Item = Mapper.Map<RegisterViewModel>(userinformation)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewData model)
        {
            if (ModelState.IsValid)
            {
                UserPhone phone =
                    _db.UserPhones.SingleOrDefault(x => x.UserID == model.Item.ID && x.ConfirmCode == model.Item.ConfirmCode);

                if (phone == null)
                {
                    ModelState.AddModelError("ConfirmCode", "Не верный код подтверждения");
                    return View(model);
                }


                if (_db.Users.Any(x => x.UserName == model.Item.UserName))
                {
                    ModelState.AddModelError("UserName", "Такой пользователь уже есть.");
                    return View(model);
                }

                phone.Confirm = true;

                UserInformation entity = phone.User;

                entity.FirstName = model.Item.FirstName;
                entity.LastName = model.Item.LastName;

                var user = new User { UserName = model.Item.UserName, UserInformation = entity };
                IdentityResult result = _userManager.Create(user, model.Item.Password);
                if (result.Succeeded)
                {
                    SignIn(user, true);

                    _db.SaveChanges();

                    return RedirectToAction("Index", "Help");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("UserError", error);
                }
            }

            return View(model);
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity =
                await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }
        private void SignIn(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity =
                _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }


        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserInformation userinformation = await _db.UserInformations.FindAsync(id);
            
            if (userinformation == null)
                return HttpNotFound();

            var model = new DetailsViewData
            {
                Item = Mapper.Map<UserInformationViewModel.Summary>(userinformation)
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Reset(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UserInformation userinformation = await _db.UserInformations.FindAsync(id);
            
            if (userinformation == null)
                return HttpNotFound();

            var model = new ResetViewData()
            {
                Item = Mapper.Map<ResetPasswordModel>(userinformation)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset(ResetViewData model)
        {
            if (!ModelState.IsValid)
                return View(model);

            UserPhone phone =
                _db.UserPhones.SingleOrDefault(x => x.UserID == model.Item.ID && x.ConfirmCode == model.Item.ConfirmCode);

            if (phone == null)
            {
                ModelState.AddModelError("ConfirmCode", "Не верный код подтверждения");
                return View(model);
            }

            phone.Confirm = true;

            UserInformation entity = phone.User;

            _userManager.RemovePassword(entity.User.Id);

            IdentityResult result = await _userManager.AddPasswordAsync(entity.User.Id, model.Item.Password);

            await SignInAsync(entity.User, true);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}