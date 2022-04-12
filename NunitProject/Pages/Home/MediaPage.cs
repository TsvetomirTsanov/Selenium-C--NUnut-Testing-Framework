using OpenQA.Selenium;
using NunitProject.other;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace NunitProject.Pages.Home
{
    public class MediaPage : BasePage
    {
        protected override string Url => ConfigurationManager.AppSettings["StrypesEuMediaUrl"];

        public MediaPage(IWebDriver driver)
            : base(driver)
        {
        }

        public SelectElement PostTypeDropdown => new SelectElement(this.ElementHelper.Find(By.CssSelector("[data-id=\"99507a0\"] select")));

        public SelectElement TopicDropdown => new SelectElement(this.ElementHelper.Find(By.CssSelector("[data-id=\"5fb33b8\"] select")));

        public IWebElement TextFIeldNew => this.ElementHelper.Find(By.CssSelector("[name=q]"));

        public IEnumerable<IWebElement> Brochures => this.ElementHelper.FindAll(By.CssSelector("article.segments-brochure"), ToBe.AllPresent);
    }
}