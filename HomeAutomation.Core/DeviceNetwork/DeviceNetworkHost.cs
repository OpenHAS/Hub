namespace HomeAutomation.Core.DeviceNetwork
{
    public class DeviceNetworkHost
    {
        internal void SendDeviceNetworkDiagnostics(IDeviceNetwork deviceNetwork, object diagnostics)
        {
            HomeAutomationSystem.ControllerHost.DeviceNetworkDiagnosticsReceived(deviceNetwork, diagnostics);
        }

        public void DeviceStateReceived(DeviceState deviceState)
        {
            Gateway.DeviceStateReceived(deviceState);
        }
    }
}
