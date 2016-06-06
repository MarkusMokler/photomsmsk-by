using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class MasterclassControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToMasterclasses()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToMasterclasses();

            Assert.AreEqual("Мастер-классы - все для фото", _driver.Title);
        }
    }
}
