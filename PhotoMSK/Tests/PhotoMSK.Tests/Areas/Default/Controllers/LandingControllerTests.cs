using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class LandingControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public void Photographer()
        {
            LandingController controller = new LandingController();
            var result = controller.Photographer() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}