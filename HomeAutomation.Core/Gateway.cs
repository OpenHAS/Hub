namespace HomeAutomation.Core
{
    internal class Gateway
    {
        /// <summary>
        /// Invoked by the controller host. Executes a command on the device network.
        /// </summary>
        /// <param name="command"></param>
        internal static void ExecuteDeviceCommand(DeviceCommand command)
        {
            IDeviceNetwork deviceNetwork = HomeAutomationSystem.DeviceNetworkRegistry.GetNetworkByName(command.DeviceNetworkName);
            deviceNetwork?.ExecuteCommand(command);
        }

        internal static void DeviceStateReceived(DeviceState deviceState)
        {
            HomeAutomationSystem.ControllerHost.DeviceStateReceived(deviceState);
        }
    }
}
