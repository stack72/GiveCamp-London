using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class FaqControllerTests
    {
        [TestCase("charities", "FAQ-Charities")]
        [TestCase("developers", "FAQ-Developers")]
        [TestCase("eventstaff", "FAQ-EventStaff")]
        [TestCase("sponsors", "FAQ-Sponsors")]
        [TestCase("", "")]
        public void FAQAction_Passing_Id_Returns_Specific_View(string id, string expectedView)
        {
            //Act
            var controller = new FAQController();
            var result = controller.FAQ(id);

            //Assert
            result.AssertViewRendered().Equals(expectedView);
        }

        public void FAQAction_Passing_CompletelyRandomId_Redirects()
        {
            //Act
            var controller = new FAQController();
            var result = controller.FAQ("twitter");

            //Assert
            result.AssertActionRedirect();
        }
    }
}
