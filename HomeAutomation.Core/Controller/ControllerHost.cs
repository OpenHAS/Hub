using HomeAutomation.Core.Controller;
using System;
using HomeAutomation.Core.Diagnostics;

namespace HomeAutomation.Core
{
    /// <summary>
    /// Provides an well known interface for the controllers to interact with the gateway.
    /// </summary>
    public class ControllerHost
    {
        public event EventHandler<ControllerDiagnosticsEventArgs> OnControllerDiagnosticsReceived;

        public event EventHandler<DeviceNetworkDiagnosticsEventArgs> OnDeviceNetworkDiagnosticsReceived;

        public event EventHandler<DeviceStateEventArgs> OnDeviceStateReceived;

        public event EventHandler<StatisticsEventArgs> OnStatisticsReceived;

        public void ExecuteDeviceCommand(DeviceCommand command)
        {
            Gateway.ExecuteDeviceCommand(command);
        }

        /// <summary>
        /// Sends statistics information about the home automation system.
        /// </summary>
        /// <param name="systemStatistics"></param>
        internal void StatisticsReceived(Statistics systemStatistics)
        {
            OnStatisticsReceived?.Invoke(this, new StatisticsEventArgs(systemStatistics));
        }

        /// <summary>
        /// Sends diagnostics information to other controllers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="diagnostics"></param>
        internal void ControllerDiagnosticsReceived(IController sender, object diagnostics)
        {
            OnControllerDiagnosticsReceived?.Invoke(sender, new ControllerDiagnosticsEventArgs(diagnostics));
        }

        internal void DeviceStateReceived(DeviceState deviceState)
        {
            OnDeviceStateReceived?.Invoke(this, new DeviceStateEventArgs(deviceState));
        }

        internal void DeviceNetworkDiagnosticsReceived(IDeviceNetwork deviceNetwork, object diagnostics)
        {
            OnDeviceNetworkDiagnosticsReceived?.Invoke(deviceNetwork, new DeviceNetworkDiagnosticsEventArgs(diagnostics));
        }
    }
}
