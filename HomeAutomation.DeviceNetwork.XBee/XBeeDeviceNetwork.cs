using HomeAutomation.Core;
using HomeAutomation.Core.Scheduler;
using HomeAutomation.DeviceNetwork.XBee.FrameProcessor;
using HomeAutomation.Logging;
using MosziNet.XBee;
using MosziNet.XBee.Frame;
using System;
using HomeAutomation.Core.DeviceNetwork;

namespace HomeAutomation.DeviceNetwork.XBee
{
    /// <summary>
    /// Defines the network that is powered by XBee devices.
    /// </summary>
    public class XBeeDeviceNetwork : IDeviceNetwork, IScheduledTask
    {
        private IXBeeService xbeeService;
        private IXBeeSerialPort serialPort;
        private DeviceNetworkHost deviceNetworkHost;

        public XBeeDeviceNetwork(IXBeeSerialPort serialPort)
        {
            this.serialPort = serialPort;
            xbeeService = new XBeeService(serialPort);

            xbeeService.MessageReceived += XbeeService_MessageReceived;
        }

        /// <summary>
        /// Returns the known device types for this device network.
        /// </summary>
        public DeviceTypeDescription[] AvailableDeviceTypes
        {
            get
            {
                // we don't support by default any devices. add later using DeviceTypeRegistry
                return new DeviceTypeDescription[] { };
            }
        }

        /// <summary>
        /// Sends a command to the XBee network.
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(DeviceCommand command)
        {
            IXBeeDevice device = HomeAutomationSystem.DeviceRegistry.GetDeviceById(this, command.DeviceID) as IXBeeDevice;

            device?.ExecuteCommand(command);
        }

        public void Initialize(DeviceNetworkHost deviceNetworkHost)
        {
            this.deviceNetworkHost = deviceNetworkHost;
        }

        /// <summary>
        /// A new device state is ready to be published, announce it
        /// </summary>
        /// <param name="deviceState"></param>
        internal void AnnounceDeviceState(DeviceState deviceState)
        {
            deviceNetworkHost.DeviceStateReceived(deviceState);
        }

        /// <summary>
        /// Returns the <see cref="IXBeeService"/> instance that can be used to send frames to the network.
        /// </summary>
        public IXBeeService XBeeService
        {
            get
            {
                return xbeeService;
            }
        }

        private void XbeeService_MessageReceived(object sender, IXBeeFrame e)
        {
            Type frameType = e.GetType();

            IXBeeFrameProcessor processor = XBeeFrameProcessorFactory.GetProcessorByFrameType(frameType);

            if (processor != null)
            {
                processor.ProcessFrame(this, e);
            }
            else
            {
                Log.Debug("Dropping frame with type " + frameType.Name + " as no suitable processor is found.");
            }
        }

        public void TimeElapsed()
        {
            xbeeService.ProcessXBeeMessages();
        }

        public object GetUpdatedDiagnostics()
        {
            return new XBeeNetworkDiagnostics()
            {
                XBeeMessageSentCount = XBeeStatistics.MessagesSent,
                XBeeMessageReceiveCount = XBeeStatistics.MessagesReceived,
                IsSerialPortConnected = serialPort.SerialPortConnected
            };
        }
    }
}
