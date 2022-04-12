using OpenQA.Selenium;
using NunitProject.other;
using System.Collections.Generic;
using NunitProject.other.CustomControls;
using System.Configuration;

namespace NunitProject.Pages.Home
{
    public class HomePage : BasePage
    {
        protected override string Url => ConfigurationManager.AppSettings["StrypesEuUrl"];

        public HomePage(IWebDriver driver)
            : base(driver)
        {
        }

        private readonly string ScrollToTopIconSelector = "ast-scroll-top-icon";

        // OurServivceSection
        public IEnumerable<IWebElement> OurServiceSectionImages => this.ElementHelper.FindAll(By.CssSelector("[data-id=\"33324c7\"] [data-widget_type=\"image.default\"] img"), ToBe.AllPresent);

        public IEnumerable<IWebElement> OurServiceSectionImageUrls => this.ElementHelper.FindAll(By.CssSelector("[data-id=\"33324c7\"] [data-widget_type=\"image.default\"] a"), ToBe.AllPresent);

        public IEnumerable<IWebElement> OurServiceSectionLinks => this.ElementHelper.FindAll(By.CssSelector("[data-id=\"33324c7\"] [data-widget_type=\"heading.default\"] a"), ToBe.AllPresent);

        // WorkPartnerSection
        public Swiper WorkPartnersSection => new Swiper("[data-id=\"e0d8417\"]", this.ElementHelper, 5);

        public IWebElement ScrollToTopButton => this.ElementHelper.Find(By.ClassName(this.ScrollToTopIconSelector), ToBe.AllPresent);
        //{
        //    get
        //    {

               
        //    }//=> this.ElementHelper.Find(By.ClassName(this.ScrollToTopIconSelector));

        //}
        public bool IsScrollToTopButtonVisible()
        {
            return this.ElementHelper.CheckPresenceOfElements(By.ClassName(this.ScrollToTopIconSelector)) && this.ScrollToTopButton.Displayed;
            
        }

        public bool IsScrolledToPageTop()
        {
            this.ElementHelper.WaitForPageToLoad();
            return this.ElementHelper.GetScrolledHeight() == 0;
        }

        public void ScrollVerticallyBy(int scrollBy)
        {
            this.ElementHelper.ScrollTo(0, scrollBy);
            this.ElementHelper.WaitForPageToLoad();
        }
    }
}