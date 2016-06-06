using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Routes;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class ModelControllerTests : IoCSupportedTest
    {

        [TestMethod]
        public void Index()
        {
            ModelController controller = Resolve<ModelController>();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            ModelController controller = Resolve<ModelController>();
            var result = controller.Details(null);

            Assert.IsInstanceOfType(result, new HttpStatusCodeResult(HttpStatusCode.BadRequest).GetType());
        }
    }
}