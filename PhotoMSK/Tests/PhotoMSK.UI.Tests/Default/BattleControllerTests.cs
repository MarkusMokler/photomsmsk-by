using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class BattleControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToBattles()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToBattles();

            Assert.AreEqual("Фотобитвы - все для фото", _driver.Title);
        }
    }
}
