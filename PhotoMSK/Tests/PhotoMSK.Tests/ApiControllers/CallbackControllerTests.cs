using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Api.v1;


namespace PhotoMSK.Tests.ApiControllers
{
    [TestClass]
    public class CallbackControllerTests
    {
        [TestMethod]
        public void Post()
        {
            CallbackController controller = new CallbackController();

           var httpResult =  controller.Post(new CallbackModel()
            {
                RouteID = new Guid("094b1120-b1de-41cb-b6df-7b64173109c3"),
                Phone = "8-029-5-82-95"
            });
        }
    }
}
