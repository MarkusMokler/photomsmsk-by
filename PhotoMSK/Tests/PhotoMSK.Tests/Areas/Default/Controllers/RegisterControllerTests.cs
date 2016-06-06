using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;
using PhotoMSK.Areas.Default.Models;
using PhotoMSK.Areas.Default.ViewData.Register;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class RegisterControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public void Index()
        {
            RegisterController controller = Resolve<RegisterController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateWithNewPhoneNumber()
        {
            const string phoneNumber = "+375 44 000-00-00";
            RegisterController controller = Resolve<RegisterController>();
            RedirectToRouteResult result = controller.Create(phoneNumber) as RedirectToRouteResult;

            Assert.AreEqual("Edit", result.RouteValues.Values.First(x => x == "Edit").ToString());
            Assert.AreEqual("Register", result.RouteValues.Values.First(x => x == "Register").ToString());
        }

        [TestMethod]
        public void CreateWithNotNewPhoneNumber()
        {
            const string phoneNumber = "+375 44 000-00-01";
            RegisterController controller = Resolve<RegisterController>();
            RedirectToRouteResult result = controller.Create(phoneNumber) as RedirectToRouteResult;

            Assert.AreEqual("Reset", result.RouteValues.Values.First(x => x == "Reset").ToString());
            Assert.AreEqual("Register", result.RouteValues.Values.First(x => x == "Register").ToString());
        }

        [TestMethod]
        public void Edit()
        {
            using (AppContext db = new AppContext())
            {
                var phone = db.Phones.First(x => x.Number == "+375440000000");

                RegisterController controller = Resolve<RegisterController>();
                var result = controller.Edit(phone.UserPhone.User.ID) as ViewResult;
                
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void EditPost()
        {
            using (AppContext db = new AppContext())
            {
                var phone = db.Phones.First(x => x.Number == "+375440000000");

                RegisterController controller = Resolve<RegisterController>();
                UserInformation userinformation = db.UserInformations.Find(phone.UserPhone.User.ID);

                var model = new EditViewData()
                {
                    Item = Mapper.Map<RegisterViewModel>(userinformation)
                };

                var result = controller.Edit(model);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void Reset()
        {
            using (AppContext db = new AppContext())
            {
                RegisterController controller = Resolve<RegisterController>();
                var phone = db.Phones.First(x => x.Number == "+375440000001");
                var result = controller.Reset(phone.UserPhone.User.ID);

                var viewResult = result.Result as ViewResult;

                Assert.IsNotNull(viewResult);
            }
        }

        [TestMethod]
        public void ResetPost()
        {
            using (AppContext db = new AppContext())
            {
                RegisterController controller = Resolve<RegisterController>();
                var phone = db.Phones.First(x => x.Number == "+375440000001");
                UserInformation userinformation = db.UserInformations.Find(phone.UserPhone.User.ID);

                var model = new ResetViewData()
                {
                    Item = Mapper.Map<ResetPasswordModel>(userinformation)
                };

                var result = controller.Reset(model);
                var finalResult = result.Result as ViewResult;

                Assert.IsNotNull(finalResult);
            }
        }
    }
}