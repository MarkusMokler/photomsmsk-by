using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class PlaceControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToPlaces()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPlaces();

            Assert.AreEqual("Index - все для фото", _driver.Title);
        }
    }
}
