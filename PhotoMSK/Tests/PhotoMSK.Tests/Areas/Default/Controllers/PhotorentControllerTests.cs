using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PhotorentControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public async void Index()
        {
            PhotorentController controller = Resolve<PhotorentController>();
            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Details()
        {
            PhotorentController controller = Resolve<PhotorentController>();
            var result = await controller.Details("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Catalog()
        {
            PhotorentController controller = Resolve<PhotorentController>();
            var result = await controller.Catalog("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}