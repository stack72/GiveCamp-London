using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace GiveCampStarterKit.Configuration
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                return (IController)ObjectFactory.GetInstance(controllerType);
            }
            return null;
        }
    }
}
