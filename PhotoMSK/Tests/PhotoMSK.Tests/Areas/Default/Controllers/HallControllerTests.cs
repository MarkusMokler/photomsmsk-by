using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Areas.Default.Controllers;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class HallControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public void Index()
        {
            HallController controller = new HallController();
            var index = controller.Index() as ViewResult;

            Assert.IsNotNull(index);
        }

        [TestMethod]
        public void PhotostudioHalls()
        {
            HallController controller = new HallController();
            var index = controller.PhotostudioHalls("photolab") as ViewResult;

            Assert.IsNotNull(index);
        }

        [TestMethod]
        public void Details(Guid? id)
        {
            HallController controller = new HallController();
            var index = controller.Details(null) as ViewResult;

            Assert.IsNotNull(index);
        }
    }
}