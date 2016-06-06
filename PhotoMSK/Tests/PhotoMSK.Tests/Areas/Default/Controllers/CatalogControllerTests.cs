using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoMSK.Areas.Default.Controllers;

namespace PhotoMSK.Tests.Areas.Default.Controllers
{
    [TestClass]
    public class CatalogControllerTests : IoCSupportedTest
    {
        private ControllerContext Context;
        private HttpContextBase _httpContex;

        [TestInitialize]
        public void Initialize()
        {
            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            HttpCookieCollection cookie = new HttpCookieCollection();
            request.Setup(x => x.Cookies).Returns(cookie);
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            var requestContext = new Mock<RequestContext>();
            requestContext.Setup(x => x.HttpContext).Returns(context.Object);

            var t = new Hashtable();

            context.Setup(x => x.Items).Returns(t);

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);

            user.Setup(ctx => ctx.Identity).Returns(identity.Object);

            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("test");

            request.Setup(req => req.Url).Returns(new Uri("http://www.google.com"));
            request.Setup(req => req.RequestContext).Returns(requestContext.Object);
            requestContext.Setup(x => x.RouteData).Returns(new RouteData());
            request.SetupGet(req => req.Headers).Returns(new NameValueCollection());

            principal.SetupGet(x => x.Identity.IsAuthenticated).Returns(false);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

            context.Setup(x => x.Request).Returns(request.Object);
            controllerContext.Setup(x => x.HttpContext).Returns(context.Object);
            Context = controllerContext.Object;

            HttpContextFactory.SetCurrentContext(context.Object);
        }

        [TestMethod]
        public void Index()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;

            var index = controller.Index("ftt") as ViewResult;

            Assert.IsNotNull(index);
        }
        [TestMethod]
        public void Category()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Category("штативы", "ftt") as ViewResult;

            Assert.IsNotNull(index);
        }
        [TestMethod]
        public void Details()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Details("штативы", "NikonD800body", "ftt") as ViewResult;

            Assert.IsNotNull(index);
        }
        [TestMethod]
        public void Indexwl()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Indexwl("ftt.by") as ViewResult;

            Assert.IsNotNull(index);
        }
        [TestMethod]
        public void Categorywl()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Categorywl("штативы", "ftt.by") as ViewResult;

            Assert.IsNotNull(index);
        }
        [TestMethod]
        public void Detailswl()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Detailswl("штативы", "D800", "ftt.by") as ViewResult;

            Assert.IsNotNull(index);
        }

        [TestMethod]
        public void Cart()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;
            var index = controller.Cart("ftt") as ViewResult;

            Assert.IsNotNull(index);
        }

        [TestMethod]
        public void Cartwl()
        {
            CatalogController controller = Resolve<CatalogController>();
            controller.ControllerContext = Context;

            var index = controller.Cartwl("ftt.by") as ViewResult;

            Assert.IsNotNull(index);
        }
        public class HttpContextFactory
        {
            private static HttpContextBase m_context;
            public static HttpContextBase Current
            {
                get
                {
                    if (m_context != null)
                        return m_context;

                    if (HttpContext.Current == null)
                        throw new InvalidOperationException("HttpContext not available");

                    return new HttpContextWrapper(HttpContext.Current);
                }
            }

            public static void SetCurrentContext(HttpContextBase context)
            {
                m_context = context;
            }
        }
    }
    public class HttpContextFactory
    {
        private static HttpContextBase m_context;
        public static HttpContextBase Current
        {
            get
            {
                if (m_context != null)
                    return m_context;

                if (HttpContext.Current == null)
                    throw new InvalidOperationException("HttpContext not available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            m_context = context;
        }
    }

}