using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using NunitProject.Pages.Home;
using System.Configuration;

namespace NunitProject.Tests.Home
{
    [TestFixture]
    public class MediaPageTests : BaseTest
    {
        private HomePage page;

		[Test]
		public void Verify_OurServicesSection_HaveCorrectTextUrlsAndImages()
		{
			// Arrange.
			this.page.GoTo();

			// Assert.
			IEnumerable<KeyValuePair<string, string>> linksData = this.page.OurServiceSectionLinks.Select(x => new KeyValuePair<string, string>(x.Text, x.GetAttribute("href"))).ToList();
			List<string> imageSourceUrl = this.page.OurServiceSectionImages.Select(x => x.GetAttribute("src")).ToList();
			List<string> imageRedirectUrl = this.page.OurServiceSectionImageUrls.Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.OurServicesLinks, linksData);
            CollectionAssert.AreEquivalent(HomePageConstants.OurServicesImgSourceUrl, imageSourceUrl);
			CollectionAssert.AreEquivalent(HomePageConstants.OurServicesImgRedirectUrl, imageRedirectUrl);
		}

		[Test]
		public void Verify_WorkPartnersSection_WhenSwipingToLeftOrRight()
		{
			// Arrange.
			int visibleSlidesCount = 5;
			this.page.GoTo();

			// Assert urls
			var workPartnersSection = this.page.WorkPartnersSection;

			List<string> companyUrls = workPartnersSection.AllSlides.Select(x => x.GetAttribute("href")).ToList();
			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls, companyUrls);

			// Assert Initial State
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(0, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);

			// Assert State After Swipe to Left
			workPartnersSection.SwipeLeft();
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(1, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);

			// Assert State After Swipe to Right
			workPartnersSection.SwipeRight();
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(0, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);
		}

		[Test]
		public void Verify_WorkPartnersSection_DragAndDropToLeftOrRight()
		{
			// Arrange.
			int visibleSlidesCount = 5;
			this.page.GoTo();

			// Assert urls
			var workPartnersSection = this.page.WorkPartnersSection;

			List<string> companyUrls = workPartnersSection.AllSlides.Select(x => x.GetAttribute("href")).ToList();
			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls, companyUrls);

			// Assert Initial State
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(0, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);

			// Assert State After Swipe to Left
			workPartnersSection.DragAndDropToLeft(1);
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(1, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);

			// Assert State After Swipe to Right
			workPartnersSection.DragAndDropToRight(1);
			companyUrls = workPartnersSection.GetVisibleSlides().Select(x => x.GetAttribute("href")).ToList();

			CollectionAssert.AreEquivalent(HomePageConstants.WorkWithCompanyUrls.GetRange(0, visibleSlidesCount), companyUrls);
			Assert.AreEqual(visibleSlidesCount, workPartnersSection.GetVisibleSlides().Where(x => x.Displayed == true).ToList().Count);
		}

		[Test]
		public void Verify_ScrollToPageTopButtonWorks()
		{
			// Arrange.
			int expectedHeightForButtonToShow = 301;
			this.page.GoTo();

			// Act & Assert
			Assert.IsFalse(this.page.IsScrollToTopButtonVisible(), "Button is present!");

			this.page.ScrollVerticallyBy(expectedHeightForButtonToShow);
			Assert.IsTrue(this.page.IsScrollToTopButtonVisible(), "Button is not present!");

			this.page.ScrollToTopButton.Click();
			Assert.IsTrue(this.page.IsScrolledToPageTop(), "Page is not scrolled to the top!");
		}

		protected override void TestSetUp()
        {
            base.TestSetUp();
            this.page = new HomePage(this.driver);
        }
    }
}
