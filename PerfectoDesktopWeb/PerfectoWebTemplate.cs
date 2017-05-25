using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PerfectoWebTemplate
{
    /// <summary>
    /// Perfecto Desktop Web Using Selenium WebDriver:
    /// This project demonstrate simply how to open a Desktop Web
    /// machine within your Perfecto Lab in the cloud and running your tests
    /// </summary>
    [TestClass]
    public class PerfectoWebTemplate
    {
        private RemoteWebDriver driver;

        [TestInitialize]
        public void PerfectoOpenConnection()
        {
            var host = Environment.GetEnvironmentVariable("host");
            var user = Environment.GetEnvironmentVariable("user");
            var pass = Environment.GetEnvironmentVariable("pass");

            // For more capabilities and supported platforms, see http://developers.perfectomobile.com/display/PD/Supported+Platforms
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "Windows");
            capabilities.SetCapability("platformVersion", "10");
            capabilities.SetCapability("browserName", "Chrome");
            capabilities.SetCapability("browserVersion", "latest");
            capabilities.SetCapability("resolution", "1280x1024");
            capabilities.SetCapability("user", user);
            capabilities.SetCapability("password", pass);

            var url = new Uri(string.Format("http://{0}/nexperience/perfectomobile/wd/hub", host));
            driver = new RemoteWebDriver(url, capabilities);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));

        }

        [TestCleanup]
        public void PerfectoCloseConnection()
        {
            driver.Close();
            driver.Quit();
        }

        [TestMethod]
        public void WebDriverTestMethod()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }
    }
}
