using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class PageControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToPublics()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPublics();

            Assert.AreEqual("- все для фото", _driver.Title);
        }
    }
}
