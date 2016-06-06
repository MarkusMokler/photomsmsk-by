using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class BattleControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public void Index()
        {
            var controller = Resolve<BattleController>();
            var viewResult = controller.Index() as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}