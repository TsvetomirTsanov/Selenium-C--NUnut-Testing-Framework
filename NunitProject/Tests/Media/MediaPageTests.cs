using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using NunitProject.Pages.Home;

namespace NunitProject.Tests.Media
{
    [TestFixture]
    public class MediaPageTests : BaseTest
    {
        private MediaPage page;

        [Test]
        public void Verify_DropdownOptions()
        {
            // Arrange.
            List<string> expectedPostTypeOptions = new List<string> { "All Post Type", "Blog (24)", "News (8)", "Whitepaper (7)", "Brochure (4)", "Case study (1)", "Video (1)" };
            List<string> expectedTopicOptions = new List<string> { "Topic", "nearsurance (4)", "Remote diagnostics (3)" };

            this.page.GoTo();

            // Assert
            Assert.AreEqual(expectedPostTypeOptions.FirstOrDefault(), this.page.PostTypeDropdown.SelectedOption.Text);
            Assert.AreEqual(expectedTopicOptions.FirstOrDefault(), this.page.TopicDropdown.SelectedOption.Text);

            CollectionAssert.AreEquivalent(expectedPostTypeOptions, this.page.PostTypeDropdown.Options.Select(x => x.Text));
            CollectionAssert.AreEquivalent(expectedTopicOptions, this.page.TopicDropdown.Options.Select(x => x.Text));
        }

        [Test]
        public void SelectBrochureOption_VerifyChanges()
        {
            // Arrange.
            this.page.GoTo();

            // Act.
            this.page.PostTypeDropdown.SelectByText("Brochure (4)");

            // Assert
            Assert.AreEqual("Brochure (4)", this.page.PostTypeDropdown.SelectedOption.Text);

            Assert.IsTrue(this.page.GetUrl().Contains("?_filter_by_post=brochure"), "Url is not as expected");
            
            Assert.AreEqual(4, this.page.Brochures.Count());
            
            Assert.AreEqual(2, this.page.TopicDropdown.Options.Count()); 
            Assert.AreEqual("Topic", this.page.TopicDropdown.SelectedOption.Text);
            Assert.AreEqual("Remote diagnostics (1)", this.page.TopicDropdown.Options.LastOrDefault().Text);
        }

        protected override void TestSetUp()
        {
            base.TestSetUp();
            this.page = new MediaPage(this.driver);
        }
    }
}
