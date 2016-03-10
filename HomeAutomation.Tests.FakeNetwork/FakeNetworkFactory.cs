using HomeAutomation.Core;
using HomeAutomation.Core.Network;
using System.Collections.Generic;

namespace HomeAutomation.Tests.FakeNetwork
{
    public class FakeNetworkFactory : IDeviceNetworkFactory
    {
        public IDeviceNetwork CreateDeviceNetwork(Dictionary<string, string> configuration)
        {
            return new FakeNetwork();
        }
    }
}
