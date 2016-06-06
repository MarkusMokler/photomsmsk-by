using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PageControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public async void Index()
        {
            PageController controller = Resolve<PageController>();
            var result = await controller.Details(null) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Details()
        {
            PageController controller = Resolve<PageController>();
            var result = await controller.Details(null) as ViewResult;

            Assert.IsNotNull(result);
        }


    }
}