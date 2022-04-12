using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NunitProject.other
{
    public class ElementHelper
    {
        protected IWebDriver Driver;
        protected WebDriverWait WebDriverWait { get; set; }

        public ElementHelper(IWebDriver driver, WebDriverWait webDriverWait)
        {
            this.Driver = driver;
            this.WebDriverWait = webDriverWait;
        }

        public IWebElement Find(By by)
        {
            this.WaitForPageToLoad();
            IWebElement webElement = Driver.FindElement(by);
            return webElement;
        }

        public IWebElement Find(By by, ToBe condition)
        {
            this.WaitForElement(condition, by);
            return this.Find(by);
        }

        public IEnumerable<IWebElement> FindAll(By by)
        {
            this.WaitForPageToLoad();
            ReadOnlyCollection<IWebElement> webElements = Driver.FindElements(by);
            return webElements;
        }

        public IEnumerable<IWebElement> FindAll(By by, ToBe condition)
        {
            this.WaitForElements(condition, by);
            return this.FindAll(by);
        }

        public bool CheckPresenceOfElements(By by)
        {
            return this.FindAll(by).Count() > 0;
        }

        public void WaitForElement(ToBe condition, By by)
        {
            this.WaitForCondition(condition, by);
        }

        public void WaitForElements(ToBe condition, By by)
        {
            this.WaitForCondition(condition, by);
        }

        public void WaitForPageToLoad()
        {
            this.WebDriverWait.Until((driver) => (bool)this.ExecuteJavaScript("return document.readyState === 'complete';"));
            this.WebDriverWait.Until((driver) => (bool)this.ExecuteJavaScript("return jQuery.active == 0;"));
        }

        internal void WaitUntil(Func<IWebDriver, bool> condition)
        {
            this.WaitForPageToLoad();
            this.WebDriverWait.Until(condition);
        }

        internal void WaitUntil(Func<IWebDriver, IEnumerable<IWebElement>> condition)
        {
            this.WaitForPageToLoad();
            this.WebDriverWait.Until(condition);
        }

        private void WaitForCondition(ToBe condition, By by)
        {
            switch (condition)
            {
                case ToBe.NotPresent:
                    {
                        this.WaitUntil(this.ElementNotVisible(by));
                        break;
                    }
                case ToBe.Clickable:
                    {
                        this.WaitUntil(this.ElementIsClickable(by));
                        break;
                    }
                case ToBe.AllPresent:
                    {
                        this.WaitUntil(this.PresenceOfAllElements(by));
                        break;
                    }
                default:
                    {
                        throw new Exception("Not implemented wait until condition: " + condition);
                    }
            }
        }

        public bool ElementExists(By by)
        {
            return this.CheckPresenceOfElements(by);
        }

        public Func<IWebDriver, bool> ElementNotVisible(By by)
        {
            return (driver) =>
            {
                return !this.Find(by).Displayed;
            };
        }

        public Func<IWebDriver, bool> ElementIsClickable(By by)
        {
            return (driver) =>
            {
                if (this.ElementExists(by))
                {
                    IWebElement seleniumElement = this.Driver.FindElement(by);
                    return seleniumElement.Displayed && seleniumElement.Enabled;
                }
                return false;
            };
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            this.ExecuteJavaScript(js);
        }

        public void ScrollToView(IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                this.ScrollTo(0, element.Location.Y - 200); // Make sure element is in the view but below the top navigation pane
            }
        }

        public int GetScrolledHeight()
        {
            var js = String.Format("return window.pageYOffset;");
            return Convert.ToInt32(this.ExecuteJavaScript(js));
        }

        public void DragAndDropElement(IWebElement sourceElement, IWebElement targetElement)
        {
            var builder = new Actions(this.Driver);
            var dragAndDrop = builder.ClickAndHold(sourceElement).MoveToElement(targetElement).Release(sourceElement).Build();
            dragAndDrop.Perform();
        }

        public Func<IWebDriver, IEnumerable<IWebElement>> PresenceOfAllElements(By by)
        {
            return (driver) =>
            {
                if (this.CheckPresenceOfElements(by))
                {
                    ReadOnlyCollection<IWebElement> seleniumElements = this.Driver.FindElements(by);
                    return seleniumElements;
                }

                return null;
            };
        }

        public object ExecuteJavaScript(string script, params object[] args)
        {
            object result = (this.Driver as IJavaScriptExecutor).ExecuteScript(script, args);
            return result;
        }
    }
}