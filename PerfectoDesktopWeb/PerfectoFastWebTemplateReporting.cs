using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Reportium.test;
using Reportium.test.Result;
using Reportium.client;
using Reportium.model;

namespace PerfectoFastWebTemplateReporting
{
    /// <summary>
    /// This project demonstrate simply how to open a Desktop Web
    /// machine within your Perfecto Lab in the cloud and running your tests
    ///
    /// The project uses Perfecto Turbo Web, for more information follow the instructions at:
    /// http://developers.perfectomobile.com/display/PD/Turbo+Web+Automation
    /// </summary>
    [TestClass]
    public class PerfectoFastWebTemplateReporting
    {
        private RemoteWebDriver driver;
        private ReportiumClient reportiumClient;

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

            var url = new Uri(string.Format("http://{0}/nexperience/perfectomobile/wd/hub", host));
            driver = new RemoteWebDriver(url, capabilities);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));

            // Reporting client. For more details, see http://developers.perfectomobile.com/display/PD/Reporting
            PerfectoExecutionContext perfectoExecutionContext = new PerfectoExecutionContext.PerfectoExecutionContextBuilder()
                    .withProject(new Project("My Project", "1.0"))
                    .withJob(new Job("My Job", 45))
                    .withContextTags(new[] { "tag1" })
                    .withWebDriver(driver)
                    .build();
            reportiumClient = PerfectoClientFactory.createPerfectoReportiumClient(perfectoExecutionContext);
        }

        [TestCleanup]
        public void PerfectoCloseConnection()
        {
            driver.Quit();

            // Retrieve the URL of the Single Test Report, can be saved to your execution summary and used to download the report at a later point
            String reportURL = reportiumClient.getReportUrl();
            Console.WriteLine(reportURL);
            // For documentation on how to export reporting PDF, see https://github.com/perfectocode/samples/wiki/reporting
            String reportPdfUrl = (String)(driver.Capabilities.GetCapability("reportPdfUrl"));

            // For detailed documentation on how to export the Execution Summary PDF Report, the Single Test report and other attachments such as
            // video, images, device logs, vitals and network files - see http://developers.perfectomobile.com/display/PD/Exporting+the+Reports
        }

        [TestMethod]
        public void WebDriverTestMethod()
        {
            try
            {
                reportiumClient.testStart("My test mame", new TestContextTags("tag2", "tag3"));

                reportiumClient.stepStart("Navigate to google");
                driver.Navigate().GoToUrl("https://www.google.com");
                reportiumClient.stepEnd();

                // Complete your test here

                reportiumClient.testStop(TestResultFactory.createSuccess());
            }
            catch (Exception e)
            {
                reportiumClient.testStop(TestResultFactory.createFailure(e.Message, e));
            }
        }
    }
}
