using System.Collections.Generic;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Keeps a register of the available device networks.
    /// </summary>
    public class DeviceNetworkRegistry
    {
        private Dictionary<IDeviceNetwork, string> deviceNetworks = new Dictionary<IDeviceNetwork, string>();
        private Dictionary<string, IDeviceNetwork> deviceNetworksByName = new Dictionary<string, IDeviceNetwork>();

        public void RegisterDeviceNetwork(IDeviceNetwork deviceNetwork, string deviceNetworkUniqueId)
        {
            deviceNetworks.Add(deviceNetwork, deviceNetworkUniqueId);
            deviceNetworksByName.Add(deviceNetworkUniqueId, deviceNetwork);

            deviceNetwork.Initialize(HomeAutomationSystem.DeviceNetworkHost);
        }

        public string GetDeviceNetworkUniqueId(IDeviceNetwork network)
        {
            return deviceNetworks[network];
        }

        public IDeviceNetwork GetNetworkByName(string deviceNetworkName)
        {
            return deviceNetworksByName[deviceNetworkName];
        }

        public IDeviceNetwork[] GetDeviceNetworks()
        {
            return new List<IDeviceNetwork>(deviceNetworks.Keys).ToArray();
        }
    }
}
