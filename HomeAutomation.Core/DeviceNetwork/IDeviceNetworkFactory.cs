using System.Collections.Generic;

namespace HomeAutomation.Core.Network
{
    public interface IDeviceNetworkFactory
    {
        IDeviceNetwork CreateDeviceNetwork(Dictionary<string, string> configuration);
    }
}
