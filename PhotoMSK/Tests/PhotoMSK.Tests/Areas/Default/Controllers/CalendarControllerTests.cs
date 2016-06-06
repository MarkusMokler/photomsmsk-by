using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Api.v1;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Models;
using PhotoMSK.Tests;


namespace PhotoMSK.Areas.Default.Controllers
{
    [TestClass]
    public class CalendarControllerTests : IoCSupportedTest
    {


        [TestMethod]
        public void Index()
        {
            CalendarController controller = Resolve<CalendarController>();

            Assert.IsNotNull(index);
        }

        [TestMethod]
        public void Details()
        {
            CalendarController controller = Resolve<CalendarController>();
            var index = controller.Details(null) as ViewResult;

            Assert.IsNotNull(index);
        }
    }
}