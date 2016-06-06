using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PhotographerControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController homeController = Resolve<HomeController>();
            homeController.Index();

            PhotographerController controller = Resolve<PhotographerController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            PhotographerController photographerController = Resolve<PhotographerController>();
            photographerController.Index();

            PhotographerController controller = Resolve<PhotographerController>();
            var result = controller.Details("solovyova_kristina") as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}