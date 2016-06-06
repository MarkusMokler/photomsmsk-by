using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class MasterclassControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public void Index()
        {
            MasterclassController controller = Resolve<MasterclassController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details(string shortcut)
        {
            MasterclassController controller = Resolve<MasterclassController>();
            var result = controller.Details("mk") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}