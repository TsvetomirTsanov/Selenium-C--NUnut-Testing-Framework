using NunitProject.other;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NunitProject.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; set; }

        protected BrowserSettings BrowserSettings { get; set; }

        protected ElementHelper ElementHelper { get; set; }
       
        protected WebDriverWait WebDriverWait { get; set; }

        protected abstract string Url { get; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            BrowserSettings = BrowserSettingsFactory.CreateBrowserSettings();
            WebDriverWait = new WebDriverWait(new SystemClock(), Driver, BrowserSettings.Timeout, BrowserSettings.PollingInterval);
            WebDriverWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            ElementHelper = new ElementHelper(driver, WebDriverWait);
        }

        public void GoTo()
        {
            this.Driver.Navigate().GoToUrl(this.Url);
            this.AcceptCookies();
        }

        public string GetUrl()
        {
            return this.Driver.Url;
        }

        private void AcceptCookies()
        {
            var cookiesBanner = By.Id("gdprc_bar");
            bool isCookiesBannerPresent = this.ElementHelper.CheckPresenceOfElements(cookiesBanner);
            if (isCookiesBannerPresent)
            {
                bool isCookiesBannerVisible = this.ElementHelper.Find(cookiesBanner).Displayed;
                if (isCookiesBannerVisible)
                {
                    IWebElement acceptButton = this.ElementHelper.Find(By.ClassName("gdprc_button"), ToBe.Clickable);
                    acceptButton.Click();
                    this.ElementHelper.WaitForElement(ToBe.NotPresent, cookiesBanner);
                }
            }
        }
    }
}

