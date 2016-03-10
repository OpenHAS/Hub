using HomeAutomation.Core;
using HomeAutomation.Logging;
using HomeAutomation.Util;
using MosziNet.XBee;
using MosziNet.XBee.Frame;

namespace HomeAutomation.DeviceNetwork.XBee.FrameProcessor
{
    /// <summary>
    /// Processes the IO data samples coming from the XBee network.
    /// </summary>
    internal class IODataSampleFrameProcessor : IXBeeFrameProcessor
    {
        static byte frameId;

        public void ProcessFrame(XBeeDeviceNetwork deviceNetwork, IXBeeFrame frame)
        {
            // get the device type from device registry. if it's not found
            // then we will ask the device to identify itself.
            IXBeeDevice device = HomeAutomationSystem.DeviceRegistry.GetDeviceById(deviceNetwork, frame.Address) as IXBeeDevice;

            if (device != null)
            {
                device.ProcessFrame(frame);

                deviceNetwork.AnnounceDeviceState(device.DeviceState);
            }
            else
            {
                StartRegisteringUnknownDevice(deviceNetwork, frame);
            }
        }

        private void StartRegisteringUnknownDevice(XBeeDeviceNetwork deviceNetwork, IXBeeFrame frame)
        {
            // check if this device is already staging
            if (!HomeAutomationSystem.DeviceRegistry.IsStagingDevice(deviceNetwork, frame.Address))
            {
                HomeAutomationSystem.DeviceRegistry.RegisterStagingDevice(deviceNetwork, frame.Address);

                AskForDeviceType(deviceNetwork, frame);
            }
        }

        private void AskForDeviceType(XBeeDeviceNetwork deviceNetwork, IXBeeFrame remoteFrame)
        {
            frameId++;
            if (frameId == 0) frameId++;

            Log.Debug($"Received a frame from an unknown device, so we are asking type ID from this device. Address: {remoteFrame.Address.ToHexString()}");

            // create the XBee frame to send
            IXBeeFrame frame = XBeeFrameBuilder.CreateRemoteATCommand(ATCommands.DD, frameId, remoteFrame.Address, remoteFrame.NetworkAddress);

            deviceNetwork.XBeeService.SendFrame(frame);
        }
    }
}
