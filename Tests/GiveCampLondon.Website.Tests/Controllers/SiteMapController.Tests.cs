using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class SiteMapControllerTests
    {
        [Test]
        public void IndexAction_Returns_View()
        {
            //Act
            var controller = new SiteMapController();
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }
    }
}
