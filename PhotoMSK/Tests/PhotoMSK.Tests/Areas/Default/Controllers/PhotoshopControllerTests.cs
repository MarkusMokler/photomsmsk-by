using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PhotoshopControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public async void Index()
        {
            PhotoshopController controller = Resolve<PhotoshopController>();
            var result =  await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Details()
        {
            PhotoshopController controller =  Resolve<PhotoshopController>();
            var result = await controller.Details("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void About()
        {
            PhotoshopController controller = Resolve<PhotoshopController>();
            var result = await controller.About("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Contact()
        {
            PhotoshopController controller = Resolve<PhotoshopController>();
            var result = await controller.Contact("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void Catalog()
        {
            PhotoshopController controller = Resolve<PhotoshopController>();
            var result = await controller.Catalog("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async void ProductDetails(string shortcut)
        {
            PhotoshopController controller = Resolve<PhotoshopController>();
            var result = await controller.ProductDetails("ftt") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}