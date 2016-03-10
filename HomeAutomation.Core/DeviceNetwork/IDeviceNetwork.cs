using HomeAutomation.Core.DeviceNetwork;

namespace HomeAutomation.Core
{
    /// <summary>
    /// The IDeviceNetwork interface defines the means on how to connect new device networks to the system. 
    /// A device network is a grouping of the devices based on how they communicate - eg. XBee device network, nRF device network, etc ...
    /// </summary>
    public interface IDeviceNetwork
    {
        /// <summary>
        /// Initializes the device network.
        /// </summary>
        /// <param name="deviceNetworkHost"></param>
        void Initialize(DeviceNetworkHost deviceNetworkHost);

        /// <summary>
        /// Sends a command to the device network.
        /// </summary>
        /// <param name="command"></param>
        void ExecuteCommand(DeviceCommand command);

        /// <summary>
        /// Forces the device network to update with its diagnostics.
        /// </summary>
        object GetUpdatedDiagnostics();
    }
}
