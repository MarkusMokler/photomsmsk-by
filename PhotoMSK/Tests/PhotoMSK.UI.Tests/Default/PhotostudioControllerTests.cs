using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class PhotostudioControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToPhotostudios()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotostudios();

            Assert.AreEqual("Фотостудии - все для фото", _driver.Title);
        }

        [TestMethod]
        public void GoToHalls()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotostudios().ToHalls();

            Assert.AreEqual("Halls - все для фото", _driver.Title);
        }
    }
}
