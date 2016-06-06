using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.Data;

namespace PhotoMSK.Tests.ApiControllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AppContext context = new AppContext();
            var halls = context.Halls.Where(x => x.Photostudio.Shortcut == "фотостейдж");

            foreach (var hall in halls)
            {
                context.Entry(hall).State = EntityState.Deleted;
            }
            context.SaveChanges();

        }
    }
}
