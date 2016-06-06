using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class PhotographerControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToPhotographers()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotographers();

            Assert.AreEqual("Index - все для фото", _driver.Title);
        }
    }
}
