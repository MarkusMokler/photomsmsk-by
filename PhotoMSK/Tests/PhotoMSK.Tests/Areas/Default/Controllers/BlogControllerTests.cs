using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Tests;

namespace PhotoMSK.Areas.Default.Controllers
{
    [TestClass]
    public class BlogControllerTests : IoCSupportedTest
    {
        [TestMethod]
        public void Index()
        {
            BlogController controller = Resolve<BlogController>();
            var index = controller.Index() as ViewResult;
            Assert.IsNotNull(index);
        }
    }
}