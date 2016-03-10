using System.Collections.Generic;

namespace HomeAutomation.Core.Controller
{
    public interface IControllerFactory
    {
        IController CreateController(Dictionary<string, string> configuration);
    }
}
