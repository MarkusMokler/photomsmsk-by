using System.Linq;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using PhotoMSK.Data;

namespace PhotoMSK.UI.Tests.Extension
{
    public static class ExtensionDriver
    {
        #region HomeControllerTests
        public static RemoteWebDriver ToPhotolab(this RemoteWebDriver driver)
        {
            var element = driver
              .FindElementsByClassName("uk-link-reset")
              .FirstOrDefault(x => x.Text == "PhotoLab STUDIO");

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver ToSomePhotostudio(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("uk-button")
                .First(x => x.Text == "Просмотреть");

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver ToSomePhotographer(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("post-image")
                .First();

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver ToSomeModel(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("grayscale")
                .First();

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver ToSomePublic(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("uk-border-circle")
                .First();

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver ToRegistrationFrame(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("uk-navbar-nav-subtitle")
                .First(x => x.Text == "ВОЙТИ\r\nРЕГИСТРАЦИЯ");

            if (element != null) element.Click();

            return driver;
        }
        #endregion

        #region PhotostudioControllerTests
        public static RemoteWebDriver ToPhotostudios(this RemoteWebDriver driver)
        {
            driver.FindElementById("Photostudio-Index").Click();

            return driver;
        }

        public static RemoteWebDriver ToHalls(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("uk-navbar-nav-subtitle")
                .First(x => x.Text == "ВСЕ ЗАЛЫ\r\nФОТОСТУДИЙ");

            if (element != null) element.Click();

            return driver;
        }
        #endregion

        #region PhotographerControllerTests
        public static RemoteWebDriver ToPhotographers(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent1", "Photographer-Index");

            return driver;
        }
        #endregion
        
        #region ModelControllerTests
        public static RemoteWebDriver ToModels(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent1", "Model-Index");

            return driver;
        }
        #endregion

        #region PageControllerTests
        public static RemoteWebDriver ToPublics(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent1", "Page-Index");

            return driver;
        }
        #endregion

        #region MasterclassControllerTests
        public static RemoteWebDriver ToMasterclasses(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent", "Masterclass-Index");

            return driver;
        }
        #endregion

        #region BattleControllerTests
        public static RemoteWebDriver ToBattles(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent", "Battle-Index");

            return driver;
        }
        #endregion

        #region PlaceControllerTests
        public static RemoteWebDriver ToPlaces(this RemoteWebDriver driver)
        {
            driver.FindElementAndClick("uk-parent", "Place-Index");

            return driver;
        }
        #endregion
        
        #region HallControllerTests
        public static RemoteWebDriver ToSomeHall(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("uk-button")
                .First(x => x.Text == "Подробнее");

            if (element != null) element.Click();

            return driver;
        }
        #endregion

        #region RegisterControllerTests
        public static RemoteWebDriver ToEnterPhonePage(this RemoteWebDriver driver)
        {
            var element = driver
                .FindElementsByClassName("btn")
                .First(x => x.Text == "Зарегистрируйтесь");

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver EnterPhoneNumber(this RemoteWebDriver driver, string phoneNumber)
        {
            var element = driver
                .FindElementById("mobile-number");

            if (element != null)
            {
                element.Click();
                element.SendKeys(phoneNumber);
                // "+375 44 000-00-00";
            }

            return driver;
        }

        public static RemoteWebDriver ToNextPage(this RemoteWebDriver driver)
        {
            var element = driver
                    .FindElementsByClassName("uk-button")
                    .First();

            if (element != null) element.Click();

            return driver;
        }

        public static RemoteWebDriver FillRegistrationForm(this RemoteWebDriver driver, string phoneNumber, string userName)
        {
            using (AppContext db = new AppContext())
            {
                var userPhone = db.UserPhones.First(x => x.Phone.Number == phoneNumber);
                var confirmNumber = userPhone.ConfirmCode.ToString();

                driver.FillInputFields("Item_ConfirmCode", confirmNumber)
                    .FillInputFields("Item_FirstName", "UserFirstName")
                    .FillInputFields("Item_LastName", "UserLastName")
                    .FillInputFields("Item_UserName", userName)
                    .FillInputFields("Item_Password", "123456");
            };

            return driver;
        }

        public static RemoteWebDriver FillInputFields(this RemoteWebDriver driver, string elementId, string input)
        {
            var element = driver.FindElementById(elementId);
            element.Click();
            element.SendKeys(input);

            return driver;
        }

        public static RemoteWebDriver FillResetPasswordForm(this RemoteWebDriver driver, string phoneNumber)
        {
            using (AppContext db = new AppContext())
            {
                var userPhone = db.UserPhones.First(x => x.Phone.Number == phoneNumber);
                var confirmNumber = userPhone.ConfirmCode.ToString();

                driver.FillInputFields("Item_ConfirmCode", confirmNumber)
                    .FillInputFields("Item_Password", "123456");
            }

            return driver;
        }
        #endregion

        public static RemoteWebDriver FindElementAndClick(this RemoteWebDriver driver, string elementClass, string elementId)
        {
            var action = new Actions(driver);
            var element = driver.FindElementByClassName(elementClass);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElementById(elementId).Click();

            return driver;
        }       
    }
}
