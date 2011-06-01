using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class ErrorControllerTests
    {
        [Test]
        public void IndexAction_Returns_View()
        {
            //Act
            var controller = new ErrorController();
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }
    }
}
