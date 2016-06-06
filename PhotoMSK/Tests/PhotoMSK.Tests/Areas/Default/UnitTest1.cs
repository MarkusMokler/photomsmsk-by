using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoMSK.Areas.Default;

namespace PhotoMSK.Tests.Areas.Default
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var headers = new NameValueCollection
{
    { "Host", "xn--80a1amg.xn--90ais" }
};
            request.Setup(x => x.Headers).Returns(headers);
            request.Setup(x => x.HttpMethod).Returns("GET");
            request.Setup(x => x.Url).Returns(new Uri("http://www.example.com"));

            context.Setup(x => x.Request).Returns(request.Object);
            WhiteLabelRoute route = new WhiteLabelRoute("{whitelabel}", "",
                new {controller = "WhiteLabel", action = "Index", id = "", whiteLabel = ""});
            route.GetRouteData(context.Object);
        }
    }
}
