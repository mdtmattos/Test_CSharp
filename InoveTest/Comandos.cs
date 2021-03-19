using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoveTest
{
    class Comandos
    {
        #region Browser
        public static IWebDriver GetBrowserRemote(IWebDriver driver, String browser)
        {
            switch (browser)
            {
                case "Firefox":
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    break;
                default:
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    break;
            }
            return driver;
        }
        public static IWebDriver GetBrowserRemote(IWebDriver driver, String browser, string uri)
        {
            switch (browser)
            {
                case "Firefox":
                    FirefoxOptions cap_firefox = new FirefoxOptions();
                    driver = new RemoteWebDriver(new Uri(uri), cap_firefox);
                    driver.Manage().Window.Maximize();
                    break;
                default:
                    ChromeOptions cap_chrome = new ChromeOptions();
                    driver = new RemoteWebDriver(new Uri(uri), cap_chrome);
                    driver.Manage().Window.Maximize();
                    break;
            }
            return driver;
        }

        [Obsolete]
        public static IWebDriver GetBrowserMobile(IWebDriver driver, String platform, String deviceName, String browserName, String uri)
        {
            switch (platform)
            {
                case "Android":
                    DesiredCapabilities cap_android = new DesiredCapabilities();
                    cap_android.SetCapability("deviceName", deviceName);
                    cap_android.SetCapability("browserName", browserName);
                    driver = new AndroidDriver<IWebElement>(new Uri(uri), cap_android);
                    break;
                default:
                    DesiredCapabilities cap_default = new DesiredCapabilities();
                    cap_default.SetCapability("deviceName", deviceName);
                    cap_default.SetCapability("browserName", browserName);
                    driver = new AndroidDriver<IWebElement>(new Uri(uri), cap_default);
                    break;
            }
            return driver;
            
        }
        #endregion

        #region JavaScript
        public static void ExecuteJavaScript(IWebDriver driver, string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);
        }
        #endregion
    }
}

