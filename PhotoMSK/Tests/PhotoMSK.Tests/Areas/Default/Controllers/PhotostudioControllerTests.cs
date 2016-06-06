using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;
using PhotoMSK.Data;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class PhotostudioControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public void Index()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Details("photolab") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details2()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Details2("photolab") as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Details3()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Details3("photolab") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IframeHalls()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.IframeHalls("photolab") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Vkontakte()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Vkontakte("photolab") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Halls()
        {
            PhotostudioController controller = Resolve<PhotostudioController>();
            var result = controller.Halls() as ViewResult;
            
            Assert.IsNotNull(result);
        }
    }
}