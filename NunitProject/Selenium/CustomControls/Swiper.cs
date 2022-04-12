using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NunitProject.other.CustomControls
{
    public class Swiper
    {
        private readonly string CssSelector;
        private readonly ElementHelper ElementHelper;

        private int LeftVisibleElementIndex;
        private int RightVisibleElementIndex;
        private int ElementsCount;
        private int MaxVisibleElements;

        public Swiper(string cssSelector, ElementHelper elementHelper, int maxVisibleElements)
        {
            this.CssSelector = cssSelector;
            this.ElementHelper = elementHelper;
            this.InitElementInfo(maxVisibleElements);
        }

        public IWebElement LeftChevron => this.ElementHelper.Find(By.CssSelector($"{this.CssSelector} i.eicon-chevron-left"), ToBe.Clickable);

        public IWebElement RightChevron => this.ElementHelper.Find(By.CssSelector($"{this.CssSelector} i.eicon-chevron-right"), ToBe.Clickable);

        public IEnumerable<IWebElement> AllSlides => this.ElementHelper.FindAll(By.XPath($"//div[contains(@data-id,'e0d8417')]//div[@class='swiper-wrapper']/div[not(contains(@class,'duplicate'))]/a"), ToBe.AllPresent);

        public void SwipeLeft()
        {
            this.ElementHelper.ScrollToView(this.RightChevron);
            this.RightChevron.Click();
            this.SetIndexesOnLeftSwipe(1);
        }

        public void SwipeRight()
        {
            this.LeftChevron.Click();
            this.SetIndexesOnRightSwipe(1);
        }

        public void DragAndDropToRight(int slideSteps)
        {
            if (slideSteps >= this.MaxVisibleElements)
            {
                throw new ArgumentOutOfRangeException(nameof(slideSteps));
            }

            IWebElement sourceElement = this.GetVisibleSlides().FirstOrDefault();
            IWebElement targetElement = this.GetVisibleSlides().ElementAt(slideSteps);

            this.ElementHelper.DragAndDropElement(sourceElement, targetElement);

            this.SetIndexesOnRightSwipe(slideSteps);
        }

        public void DragAndDropToLeft(int slideSteps)
        {
            if (slideSteps >= this.MaxVisibleElements - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(slideSteps));
            }

            IWebElement sourceElement = this.GetVisibleSlides().LastOrDefault();
            IWebElement targetElement = this.GetVisibleSlides().ElementAt(this.MaxVisibleElements - slideSteps - 1);

            this.ElementHelper.DragAndDropElement(sourceElement, targetElement);

            this.SetIndexesOnLeftSwipe(slideSteps);
        }

        public List<IWebElement> GetVisibleSlides()
        {
            return this.AllSlides.ToList().GetRange(this.LeftVisibleElementIndex, this.MaxVisibleElements);
        }

        private void InitElementInfo(int maxElements)
        {
            this.LeftVisibleElementIndex = 0;
            this.RightVisibleElementIndex = maxElements - 1;
            this.ElementsCount = this.AllSlides.Count();
            this.MaxVisibleElements = maxElements;
        }

        private void SetIndexesOnLeftSwipe(int step)
        {
            this.LeftVisibleElementIndex = this.LeftVisibleElementIndex + step >= this.ElementsCount ? this.ElementsCount + (this.LeftVisibleElementIndex - step) : this.LeftVisibleElementIndex + step;
            this.RightVisibleElementIndex = this.RightVisibleElementIndex + step >= this.ElementsCount ? this.ElementsCount + (this.RightVisibleElementIndex - step) : this.RightVisibleElementIndex + step;
        }

        private void SetIndexesOnRightSwipe(int step)
        {
            this.LeftVisibleElementIndex = this.LeftVisibleElementIndex - step < 0 ? this.ElementsCount + (this.LeftVisibleElementIndex - step) : this.LeftVisibleElementIndex - step;
            this.RightVisibleElementIndex = this.RightVisibleElementIndex - step < 0 ? this.ElementsCount + (this.RightVisibleElementIndex - step) : this.RightVisibleElementIndex - step;
        }
    }
}
