using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class RegisterControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToEnterPhonePage()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame().ToEnterPhonePage();

            Assert.AreEqual("- все для фото", _driver.Title);
        }

        [TestMethod]
        public void GoToRegistrationPage()
        {
            const string phoneNumber = "+375 44 000-00-00";

            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame()
                .ToEnterPhonePage()
                .EnterPhoneNumber(phoneNumber)
                .ToNextPage();

            Assert.AreEqual("- все для фото", _driver.Title);
        }

        [TestMethod]
        public void Registrate()
        {
            const string userName = "UserName";
            const string phoneNumber = "+375440000000";
            const string phoneNumberA = "+375 44 000-00-00";

            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame()
                .ToEnterPhonePage()
                .EnterPhoneNumber(phoneNumberA)
                .ToNextPage()
                .FillRegistrationForm(phoneNumber, userName)
                .ToNextPage();

            Assert.AreEqual("- все для фото", _driver.Title);

            RemoveTestUser(phoneNumber, userName);
        }

        [TestMethod]
        public void GoToResetPassword()
        {
            const string phoneNumberA = "+375 44 000-00-01";

            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame()
                .ToEnterPhonePage()
                .EnterPhoneNumber(phoneNumberA)
                .ToNextPage();

            Assert.AreEqual("- все для фото", _driver.Title);
        }

        [TestMethod]
        public void ResetPassword()
        {
            const string phoneNumber = "+375440000001";
            const string phoneNumberA = "+375 44 000-00-01";

            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame()
                .ToEnterPhonePage()
                .EnterPhoneNumber(phoneNumberA)
                .ToNextPage()
                .FillResetPasswordForm(phoneNumber)
                .ToNextPage();

            Assert.AreEqual("PhotoMSK - все для фото", _driver.Title);
        }

        public void RemoveTestUser(string phoneNumber, string userName)
        {
            using (AppContext db = new AppContext())
            {
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(db));         

                var user = userManager.FindByName(userName);

                var userInformation = user.UserInformation;
                userInformation.FirstName = "";
                userInformation.LastName = "";

                userManager.Delete(user);

                var phone = db.UserPhones.First(x => x.Phone.Number == phoneNumber);
                phone.Confirm = false;

                db.SaveChanges();
            }
        }
    }
}
