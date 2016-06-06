using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PhototechnicsControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public void ShopList()
        {
            PhototechnicsController controller = Resolve<PhototechnicsController>();
            var result = controller.ShopList("D800") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index()
        {
            PhototechnicsController controller = Resolve<PhototechnicsController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Details()
        {
            PhototechnicsController controller = Resolve<PhototechnicsController>();
            var result = await controller.Details("D800") as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}