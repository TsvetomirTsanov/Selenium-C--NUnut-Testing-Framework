using NUnit.Framework;
using NunitProject.other;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NunitProject.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        protected virtual void TestSetUp()
        {
            BrowserSettings browserSettings = BrowserSettingsFactory.CreateBrowserSettings();
            driver = this.SetupChromeDriver(browserSettings);
        }

        [TearDown]
        protected virtual void TestTearDown()
        {
            this.driver.Quit();
        }

        private IWebDriver SetupChromeDriver(BrowserSettings settings)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            chromeOptions.AddArguments(
                "enable-auto-reload",
                "disable-gpu",
                "ignore-certificate-errors",
                "no-sandbox-and-elevated",
                "run-without-sandbox-for-testing",
                "start-maximized",
                "window-size=1920x1080");
            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddUserProfilePreference("download.default_directory", System.IO.Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads"));
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, settings.Timeout);
        }
    }
}
