using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace PerfectoFastWebTemplate
{
    /// <summary>
    /// This project demonstrate simply how to open a Desktop Web
    /// machine within your Perfecto Lab in the cloud and running your tests
    ///
    /// The project uses Perfecto Turbo Web, for more information follow the instructions at:
    /// http://developers.perfectomobile.com/display/PD/Turbo+Web+Automation
    /// </summary>
    [TestClass]
    public class PerfectoFastWebTemplate
    {
        private RemoteWebDriver driver;

        [TestInitialize]
        public void PerfectoOpenConnection()
        {
            var host = Environment.GetEnvironmentVariable("host");
            var token = Environment.GetEnvironmentVariable("token");

            // For more capabilities and supported platforms, see http://developers.perfectomobile.com/display/PD/Supported+Platforms
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "Windows");
            capabilities.SetCapability("platformVersion", "10");
            capabilities.SetCapability("browserName", "Chrome");
            capabilities.SetCapability("browserVersion", "latest");
            capabilities.SetCapability("resolution", "1280x1024");

            // Perfecto Turbo Web requires authentication with security token, for more information see:
            // http://developers.perfectomobile.com/display/PD/Security+Token
            capabilities.SetCapability("securityToken", token);

            var url = new Uri(string.Format("http://{0}/nexperience/perfectomobile/wd/hub/fast", host));
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
