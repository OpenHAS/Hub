using HomeAutomation.Core;
using HomeAutomation.Core.Controller;
using System.Collections.Generic;

namespace HomeAutomation.Controller.Internal
{
    public class InternalControllerFactory : IControllerFactory
    {
        public IController CreateController(Dictionary<string, string> configuration)
        {
            return new InternalController();
        }
    }
}
