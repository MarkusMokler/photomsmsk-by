using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class HallControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToHallDetails()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotostudios().ToHalls().ToSomeHall();

            Assert.IsNotNull(_driver.FindElementByClassName("hall-title").Text);
        }
    }
}
