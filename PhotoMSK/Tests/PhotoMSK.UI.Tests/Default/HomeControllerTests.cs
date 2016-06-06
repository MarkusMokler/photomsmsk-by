using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoMSK.UI.Tests.Extension;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public class HomeControllerTests : AbstractTest
    {
        [TestMethod]
        public void GoToModelDetails()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToModels().ToSomeModel();

            var element = _driver.FindElementByClassName("hex-testimonial");

            Assert.IsNotNull(element);
        }

        [TestMethod]
        public void GoToPhotographerDetails()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotographers().ToSomePhotographer();

            Assert.AreEqual("Фотограф Кристина Соловьёва - все для фото", _driver.Title);
        }

        [TestMethod]
        public void GoToPublicDetails()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPublics().ToSomePublic();

            Assert.AreEqual("Паблик Photomsk: фото студии, фотографы, модели! - все для фото", _driver.Title);
        }

        [TestMethod]
        public void GoToPhotostudio()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotostudios().ToPhotolab();

            Assert.AreEqual("Фотостудия PhotoLab STUDIO - все для фото", _driver.Title);
        }

        [TestMethod]
        public void WatchPhotostudio()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToPhotostudios().ToSomePhotostudio();

            Assert.AreEqual("Фотостудия Diva studio - все для фото", _driver.Title);
        }

        [TestMethod]
        public void OpenRegistrationFrame()
        {
            _driver.Navigate().GoToUrl(Url);
            _driver.ToRegistrationFrame();

            var element = _driver
                .FindElementsByClassName("uk-modal-dialog")
                .First();

            Assert.IsNotNull(element);
        }
    }
}
