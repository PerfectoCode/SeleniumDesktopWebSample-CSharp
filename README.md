# CSharp + UnitUnitTesting Perfecto Desktop Web Sample

This code sample demonstrates how to use Perfecto Web Machines & Selenium + UnitUnitTesting in order to execute tests 
for your web applications on the cloud. 

### Quick Start: 
- Clone or download the sample:<br/> `git clone https://github.com/PerfectoCode/SeleniumDesktopWebSample-CSharp.git`
- Add your Perfecto Lab credentials within the one of the test classes:
```C#
...
var host = Environment.GetEnvironmentVariable("host");
var user = Environment.GetEnvironmentVariable("user");
var pass = Environment.GetEnvironmentVariable("pass");
... 
```
Note! you may want to use env variable for your credentials as demonstrated

- Note:exclamation: the project include 4 templates: 
    - PerfectoFastWebTemplate: template for Perfecto Turbo Web.
    - PerfectoFastWebTemplateReporting: template for Perfecto Turbo Web + DigitalZoom Reporting.
    - PerfectoWebTemplate: basic web automation template.
    - PerfectoWebTemplateReporting: same as the basic template + DigitalZoom Reporting.

- Choose one (or more) of the templates, import into your existing solution or use this solution.

- Run the project from visual studio.

### Web Capabilities: 
- To insure your tests run on Perfecto Web machines on the cloud use the capabilities as demonstrated in the code sample: <br/>
```C#
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
}
```

- More capabilities are available, read more [here](http://developers.perfectomobile.com/display/PD/Supported+Platforms).

### Perfecto Turbo Web Automation:

Perfecto's Desktop Web environment introduces an accelerated interface to Web Browser automation with its new Turbo web interface. Using this new environment will allow you to connect quicker to the browser "device" you select for automating and testing your web application.

*Click [here](http://developers.perfectomobile.com/display/PD/Turbo+Web+Automation) to read more about Turbo Web Automation.*

- To enable Turbo Web Automation in this code sample follow the instructions in the link above in order to generate authentication token.
Place the authentication token within the bTestInitialize method in one of the test's classes:
```C#
... 
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
...
```

### Perfecto DigitalZoom reporting:

Perfecto Reporting is a multiple execution digital report, that enables quick navigation within your latest build execution. Get visibility of your test execution status and quickly identify potential problems with an aggregated report.
Hone-in and quickly explore your test results all within customized views, that include logical steps and synced artifacts. Distinguish between test methods within a long execution. Add personalized logical steps and tags according to your team and organization.

*Click [here](http://developers.perfectomobile.com/display/PD/Reporting) to read more about DigitalZoom Reporting.*
