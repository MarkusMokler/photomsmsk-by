using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PlaceControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public async void Index()
        {
            PlaceController controller = Resolve<PlaceController>();
            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async void Details()
        {
            var controller = Resolve<PlaceController>();
            var result = await controller.Details(null) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}