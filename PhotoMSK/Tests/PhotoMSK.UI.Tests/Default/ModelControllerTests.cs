using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;
using Selenium;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class ModelControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToModels()
        {
            /*var selenium = new WebDriverBackedSelenium(_driver, Url);
            selenium.Start();
            selenium.Open("/");
            */
            _driver.Navigate().GoToUrl(Url);
            _driver.ToModels();

            Assert.AreEqual("Index - все для фото", _driver.Title);
        }
    }
}
