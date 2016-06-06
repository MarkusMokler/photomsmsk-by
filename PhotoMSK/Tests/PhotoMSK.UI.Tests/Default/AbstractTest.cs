using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium.Firefox;

namespace PhotoMSK.UI.Tests.Default
{
    [TestClass]
    public abstract class AbstractTest
    {
        protected FirefoxDriver _driver;

        [TestInitialize]
        public void Initialize()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
        }

        protected AbstractTest()
        {
            var request = WebRequest.Create(Url);
            request.Timeout = 10 * 60 * 1000;
            request.GetResponse();

        }


        public readonly Lazy<string> Link = new Lazy<string>(() =>
        {
            try
            {
                using (var writer = new StreamReader("c:\\photomsk\\conf.json"))
                {

                    MyData oo = JsonConvert.DeserializeObject<MyData>(writer.ReadToEnd());
                    return oo.Url;

                }
            }
            catch (Exception)
            {
                return "http://photomsk.by/";
            }

        }, true);

      
        public string Url { get { return Link.Value; } }

    }
    public class MyData
    {
        public string Url { get; set; }
    }

}
